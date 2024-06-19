using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Brands : BasePage
    {
        // ATTRIBUTES

        private Brand _brand;
        private List<Brand> _brands;
        private BrandsManager _brandsManager;
        private int _temporalBrandId;

        // Referencia a la funcion que se ejecutará luego de confirmar/cancelar un Modal
        // Se le debe pasar la masterpage como referencia por si se necesita manipular controles
        // En caso de manipular un control utilizar Helper.FindControl
        private static Action<MasterPage> _modalOkAction;
        private static Action<MasterPage> _modalCancelAction;

        //CONSTRUCT
        public Brands()
        {
            _brand = new Brand();
            _brandsManager = new BrandsManager();
            FetchBrands();
        }

        // METHODS

        private void FetchBrands()
        {
            _brands = _brandsManager.List();
        }

        private void BindBrandsRpt()
        {
            BrandsListRpt.DataSource = _brands;
            BrandsListRpt.DataBind();
        }

        private void ToggleEditMode(RepeaterItem item, bool isEditMode)
        {
            Label nameLbl = (Label)item.FindControl("BrandNameLbl");
            TextBox editTxt = (TextBox)item.FindControl("EditBrandNameTxt");
            LinkButton editBtn = (LinkButton)item.FindControl("EditBrandBtn");
            LinkButton saveBtn = (LinkButton)item.FindControl("SaveBrandBtn");
            LinkButton cancelBtn = (LinkButton)item.FindControl("CancelEditBtn");

            nameLbl.Visible = !isEditMode;
            editTxt.CssClass = isEditMode ? "form-control" : "form-control d-none";
            editBtn.Visible = !isEditMode;
            saveBtn.CssClass = isEditMode ? "p-0 text-black fs-5" : "p-0 text-black fs-5 d-none";
            cancelBtn.CssClass = isEditMode ? "p-0 text-black fs-5" : "p-0 text-black fs-5 d-none";
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchBrands();
                BindBrandsRpt();
            }
        }

        protected void NewBrandBtn_Click(object sender, EventArgs e)
        {
            _brand.Name = "Nueva Marca";
            _brands.Add(_brand);
            BindBrandsRpt();

            foreach (RepeaterItem item in BrandsListRpt.Items)
            {
                Label nameLbl = item.FindControl("BrandNameLbl") as Label;

                if (nameLbl.Text == _brand.Name)
                {
                    LinkButton deleteBtn = item.FindControl("DeleteBrandBtn") as LinkButton;
                    deleteBtn.Visible = false;
                    ToggleEditMode(item, true);
                    break;
                }
            }
        }

        private void DeleteBrandAction(MasterPage masterPage)
        {
            Brand auxBrand = _brandsManager.Read(_temporalBrandId);
            _brandsManager.Delete(auxBrand);

            FetchBrands();
            BindBrandsRpt();
            ((Admin)masterPage).ShowMasterToast("Marca eliminada con éxito!");

            // TODO: Actualizar Repeater
        }

        public override void OnModalConfirmed()
        {
            if (_modalOkAction != null)
            {
                _modalOkAction(this.Master);
                _modalOkAction = null; // Limpiar luego de usar
            }
        }

        protected void DeleteBrandBtn_Click(object sender, EventArgs e)
        {
            _temporalBrandId = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            _modalOkAction = DeleteBrandAction;

            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterModal( // Llama y muestra el modal de la Masterpage
                "Eliminar Marca",
                "Está seguro que desea eliminar la Marca?",
                true // requiere confirmación
            );
        }

        private void DeleteBrand(MasterPage masterPage)
        {
            _brandsManager.Delete(_brand);
        }

        protected void SearchBtn_Click(object sender, EventArgs e) { }

        /// <summary>
        /// Evento que se dispara cuando se hace clic en cualquier control dentro del Repeater que tenga el atributo CommandName definido.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void BrandsListRpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ToggleEditMode(e.Item, true);
            }
            else if (e.CommandName == "Save")
            {
                TextBox editTextBox = (TextBox)e.Item.FindControl("EditBrandNameTxt");
                _brand.Id = Convert.ToInt32(e.CommandArgument);
                _brand.Name = editTextBox.Text;

                if (0 < _brand.Id)
                {
                    _brandsManager.Edit(_brand);
                }
                else
                {
                    _brandsManager.Add(_brand);
                }

                ToggleEditMode(e.Item, false);
                FetchBrands();
                BindBrandsRpt();
            }
            else if (e.CommandName == "Cancel")
            {
                ToggleEditMode(e.Item, false);
                FetchBrands();
                BindBrandsRpt();
            }
        }
    }
}
