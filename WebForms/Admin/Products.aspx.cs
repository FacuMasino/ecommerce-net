using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms.Admin
{
    public partial class Products : BasePage
    {
        private ProductsManager _productsManager;

        private int _temporalProductId;

        public Products()
        {
            _productsManager = new ProductsManager();
        }

        // METHODS

        protected string PrintCategoriesCount(object categoriesList)
        {
            var categories = categoriesList as List<Category>;

            if (categories == null || categories.Count < 2)
            {
                return "";
            }

            return $" (+{categories.Count - 1})";
        }

        private void BindProductList()
        {
            ProductListRepeater.DataSource = _productsManager.List();
            ProductListRepeater.DataBind();
        }

        /// <summary>
        /// Esta funcion se utiliza solo cuándo es llamada desde otra del tipo Action
        /// </summary>
        private void BindProductList(MasterPage masterPage)
        {
            Repeater auxRpt = ((Repeater)Helper.FindControl(masterPage, "ProductListRepeater"));
            auxRpt.DataSource = _productsManager.List();
            auxRpt.DataBind();
        }

        private void CheckRequest()
        {
            foreach (string key in Request.QueryString.AllKeys)
            {
                switch (key)
                {
                    case "successDelete":
                        if (Request.QueryString[key] == "true")
                        {
                            Notify("Producto eliminado con éxito!");
                        }
                        break;
                    case "successNewProduct":
                        if (Request.QueryString[key] == "true")
                        {
                            Notify("Producto agregado con éxito!");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void Notify(string message)
        {
            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterToast(message);
        }

        private void DeleteProductAction(MasterPage masterPage)
        {
            Product auxProduct = _productsManager.Read(_temporalProductId);
            _productsManager.Delete(auxProduct);
            ((Admin)masterPage).ShowMasterToast("Producto eliminado con éxito!");
            //HttpContext.Current.Response.Redirect("Products.aspx?successDelete=true");
            BindProductList(masterPage); // Actualizar lista sin Redirect
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckRequest();
                BindProductList();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e) { }

        protected void DeleteProductLnkBtn_Click(object sender, EventArgs e)
        {
            _temporalProductId = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            ModalOkAction = DeleteProductAction;

            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterModal( // Llama y muestra el modal de la Masterpage
                "Eliminar Producto",
                "Está seguro que desea eliminar el producto?",
                true // requiere confirmación
            );
        }
    }
}
