using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms.Admin
{
    public partial class Brands : BasePage
    {
        // ATTRIBUTES

        private User _sessionUser;
        private UsersManager _usersManager;
        private Brand _brand;
        private List<Brand> _brands;
        private BrandsManager _brandsManager;
        private bool _isSearching;
        private string _textToSearch;

        public int TotalBrands
        {
            get { return _brands == null ? 0 : _brands.Count; }
        }

        //CONSTRUCT

        public Brands()
        {
            _brand = new Brand();
            _brandsManager = new BrandsManager();
            _usersManager = new UsersManager();
            FetchBrands();
        }

        // METHODS

        private void Notify(string message)
        {
            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterToast(message);
        }

        /// <summary>
        /// Notificar desde una funcion invocada por un Modal
        /// </summary>
        private void Notify(string message, MasterPage masterPage)
        {
            ((Admin)masterPage).ShowMasterToast(message);
        }

        private bool IsValidName(TextBox textBox)
        {
            InputWrapper auxWrapper = new InputWrapper(textBox, typeof(string), 2, 30, false, true);
            if (Validator.IsGoodInput(auxWrapper))
                return true;
            return false;
        }

        private void FetchSessionUser()
        {
            _sessionUser = (User)Session["user"];
        }

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

        private void CheckRequest()
        {
            foreach (string key in Request.QueryString.AllKeys)
            {
                switch (key)
                {
                    case "successDelete":
                        if (Request.QueryString[key] == "true")
                        {
                            Notify("Marca eliminada con éxito!");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetSearchState()
        {
            _isSearching = ViewState["IsSearching"] as bool? ?? false;
            _textToSearch = ViewState["TextToSearch"] as string ?? "";
        }

        private void SetSearchState(bool isSearching, string textToSearch)
        {
            ViewState["IsSearching"] = isSearching;
            ViewState["TextToSearch"] = textToSearch;
        }

        private void DeleteBrandAction(MasterPage masterPage)
        {
            if (_brandsManager.CountBrandRelations(_brand) == 0)
            {
                _brandsManager.Delete(_brand);
                HttpContext.Current.Response.Redirect("Brands.aspx?successDelete=true");
            }
            else
            {
                Notify("La Marca está en uso y no puede ser borrada.", masterPage);
            }
        }

        private void CheckUserPermissions()
        {
            if (_sessionUser == null)
            {
                Response.Redirect("/AccessDenied.aspx");
                return;
            }

            else if (!_usersManager.IsAdmin(_sessionUser))
            {
                Response.Redirect("/AccessDenied.aspx");
                return;
            }
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchSessionUser();
                CheckUserPermissions();
                CheckRequest();
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

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string filter = SearchTextBox.Text;
            GetSearchState(); // Obtiene el estado de busqueda

            // Limpiar búsqueda si ya está buscando y el texto es el mismo
            if (_isSearching && _textToSearch == filter)
            {
                // Resetear estado
                SetSearchState(false, ""); // Limpia el estado de busqueda

                // Resetear controles
                SearchBtn.Text = "<i class=\"bi bi-search\"></i>";
                SearchTextBox.Text = "";
                SearchPanel.CssClass = "input-group mb-3";

                FetchBrands();
                BindBrandsRpt();
                return;
            }

            if (2 <= filter.Length)
            {
                SearchPanel.CssClass = "input-group mb-3";
                _brands = _brands.FindAll(x => x.Name.ToUpper().Contains(filter.ToUpper()));
                SearchBtn.Text = "<i class=\"bi bi-x-circle\"></i>"; // cambia icono boton de busqueda
            }
            else
            {
                SearchPanel.CssClass = "input-group mb-3 invalid";
            }

            SetSearchState(true, filter); // Guarda el estado para saber que está buscando
            BindBrandsRpt();
        }

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

                if (!IsValidName(editTextBox))
                {
                    Notify(
                        "El nombre de Marca es inválido, debe ser entre 2 y 30 caracteres alfanuméricos."
                    );
                    return;
                }

                _brand.Id = Convert.ToInt32(e.CommandArgument);
                _brand.Name = editTextBox.Text;

                int databaseId = _brandsManager.GetId(_brand);

                if (0 < databaseId)
                {
                    _brand.Id = databaseId;
                    _brand.IsActive = true;
                }

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
            else if (e.CommandName == "Delete")
            {
                Label nameLbl = (Label)e.Item.FindControl("BrandNameLbl");
                _brand.Id = Convert.ToInt32(e.CommandArgument);
                _brand.Name = nameLbl.Text;

                ModalOkAction = DeleteBrandAction;
                Admin adminMP = (Admin)this.Master;
                adminMP.ShowMasterModal( // Llama y muestra el modal de la Masterpage
                    "Eliminar Marca",
                    "Está seguro que desea eliminar la marca?",
                    true // requiere confirmación
                );
            }
        }
    }
}
