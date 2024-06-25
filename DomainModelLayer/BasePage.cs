using System;
using System.Web.UI;

namespace DomainModelLayer
{
    public class BasePage : System.Web.UI.Page
    {
        // Estos atributos son estaticos para que no pierdan la referencia
        // A la funcion cada vez que se vuelve a instanciar la clase debido a un Postback
        private static Action<MasterPage> _modalOkAction;
        private static Action<MasterPage> _modalCancelAction;

        /// <summary>
        /// Action que referencia a la funcion que se ejecutará luego de confirmar un Modal
        /// </summary>
        protected Action<MasterPage> ModalOkAction
        {
            get { return _modalOkAction; }
            set { _modalOkAction = value; }
        }

        /// <summary>
        /// Action que referencia a la funcion que se ejecutará luego de cancelar un Modal
        /// </summary>
        protected Action<MasterPage> ModalCancelAction
        {
            get { return _modalCancelAction; }
            set { _modalCancelAction = value; }
        }

        /// <summary>
        /// Esta funcion debera ser invocada desde el evento onClick del boton Confirmar de un Modal
        /// Puede ser sobreescrita segun sea conveniente.
        /// <para>Se encarga de invocar a la funcion actual que este siendo referenciada</para>
        /// <para>en ese momento dentro de la prop ModalOkAction, pasandole como referencia</para>
        /// <para>la MasterPage desde donde se dispara el evento</para>
        /// </summary>
        public virtual void OnModalConfirmed()
        {
            if (_modalOkAction != null)
            {
                _modalOkAction(this.Master);
                _modalOkAction = null; // Limpiar luego de invocar
            }
        }

        /// <summary>
        /// Esta funcion debera ser invocada desde el evento onClick del boton Cancelar de un Modal
        /// Puede ser sobreescrita segun sea conveniente.
        /// <para>Se encarga de invocar a la funcion actual que este siendo referenciada</para>
        /// <para>en ese momento dentro de la prop ModalCancelAction, pasandole como referencia</para>
        /// <para>la MasterPage desde donde se dispara el evento</para>
        /// </summary>
        public virtual void OnModalCancelled()
        {
            if (_modalCancelAction != null)
            {
                _modalCancelAction(this.Master);
                _modalCancelAction = null; // Limpiar luego de invocar
            }
        }
    }
}
