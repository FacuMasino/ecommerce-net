using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms.Admin
{
    public partial class Categories : BasePage
    {
        // ATTRIBUTES

        private Category _category;
        private List<Category> _categories;
        private CategoriesManager _categoriesManager;
        private bool _isSearching;
        private string _textToSearch;

        public int TotalCategories
        {
            get { return _categories == null ? 0 : _categories.Count; }
        }

        // CONSTRUCT

        public Categories()
        {
            _category = new Category();
            _categoriesManager = new CategoriesManager();
            FetchCategories();
        }

        // METHODS

        private bool IsValidName(TextBox textBox)
        {
            InputWrapper auxWrapper = new InputWrapper(textBox, typeof(string), 2, 30, false, true);
            if (Validator.IsGoodInput(auxWrapper))
                return true;
            return false;
        }

        private void Notify(string message)
        {
            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterToast(message);
        }

        private void Notify(string message, MasterPage master)
        {
            Admin adminMP = (Admin)master;
            adminMP.ShowMasterToast(message);
        }

        private void FetchCategories()
        {
            _categories = _categoriesManager.List();
        }

        private void BindCategoriesRpt()
        {
            CategoriesListRpt.DataSource = _categories;
            CategoriesListRpt.DataBind();
        }

        private void BindCategoriesRpt(MasterPage masterPage)
        {
            Repeater auxRpt = ((Repeater)Helper.FindControl(masterPage, "CategoriesListRpt"));
            auxRpt.DataSource = _categories;
            auxRpt.DataBind();
        }

        private void ToggleEditMode(RepeaterItem item, bool isEditMode)
        {
            Label nameLbl = (Label)item.FindControl("CategoryNameLbl");
            TextBox editTxt = (TextBox)item.FindControl("EditCategoryNameTxt");
            LinkButton editBtn = (LinkButton)item.FindControl("EditCategoryBtn");
            LinkButton saveBtn = (LinkButton)item.FindControl("SaveCategoryBtn");
            LinkButton cancelBtn = (LinkButton)item.FindControl("CancelEditBtn");

            nameLbl.Visible = !isEditMode;
            editTxt.CssClass = isEditMode ? "form-control" : "form-control d-none";
            editBtn.Visible = !isEditMode;
            saveBtn.CssClass = isEditMode ? "p-0 text-black fs-5" : "p-0 text-black fs-5 d-none";
            cancelBtn.CssClass = isEditMode ? "p-0 text-black fs-5" : "p-0 text-black fs-5 d-none";
        }

        private void DeleteCategoryAction(MasterPage masterPage)
        {
            if (_categoriesManager.CountCategoryRelations(_category) == 0)
            {
                _categoriesManager.Delete(_category);
                Notify("La categoría fue eliminada con éxito!", masterPage);
                FetchCategories();
                BindCategoriesRpt(masterPage);
            }
            else
            {
                Notify("La categoría está en uso y no puede ser borrada.", masterPage);
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

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchCategories();
                BindCategoriesRpt();
            }
        }

        protected void NewCategoryBtn_Click(object sender, EventArgs e)
        {
            _category.Name = "Nueva categoría";
            _categories.Add(_category);
            BindCategoriesRpt();

            foreach (RepeaterItem item in CategoriesListRpt.Items)
            {
                Label nameLbl = item.FindControl("CategoryNameLbl") as Label;

                if (nameLbl.Text == _category.Name)
                {
                    LinkButton deleteBtn = item.FindControl("DeleteCategoryBtn") as LinkButton;
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

                FetchCategories();
                BindCategoriesRpt();
                return;
            }

            if (2 <= filter.Length)
            {
                SearchPanel.CssClass = "input-group mb-3";
                _categories = _categories.FindAll(x => x.Name.ToUpper().Contains(filter.ToUpper()));
                SearchBtn.Text = "<i class=\"bi bi-x-circle\"></i>"; // cambia icono boton de busqueda
            }
            else
            {
                SearchPanel.CssClass = "input-group mb-3 invalid";
            }

            SetSearchState(true, filter); // Guarda el estado para saber que está buscando
            BindCategoriesRpt();
        }

        /// <summary>
        /// Evento que se dispara cuando se hace clic en cualquier control dentro del Repeater que tenga el atributo CommandName definido.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void CategoriesListRpt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                ToggleEditMode(e.Item, true);
            }
            else if (e.CommandName == "Save")
            {
                TextBox editTxt = (TextBox)e.Item.FindControl("EditCategoryNameTxt");

                if (!IsValidName(editTxt))
                {
                    Notify(
                        "El nombre de Categoría es inválido, debe ser entre 2 y 30 caracteres alfanuméricos."
                    );
                    return;
                }

                _category.Id = Convert.ToInt32(e.CommandArgument);
                _category.Name = editTxt.Text;

                int databaseId = _categoriesManager.GetId(_category);

                if (0 < databaseId)
                {
                    _category.Id = databaseId;
                    _category.IsActive = true;
                }

                if (0 < _category.Id)
                {
                    _categoriesManager.Edit(_category);
                }
                else
                {
                    _categoriesManager.Add(_category);
                }

                ToggleEditMode(e.Item, false);
                FetchCategories();
                BindCategoriesRpt();
            }
            else if (e.CommandName == "Cancel")
            {
                ToggleEditMode(e.Item, false);
                FetchCategories();
                BindCategoriesRpt();
            }
            else if (e.CommandName == "Delete")
            {
                Label nameLbl = (Label)e.Item.FindControl("CategoryNameLbl");
                _category.Id = Convert.ToInt32(e.CommandArgument);
                _category.Name = nameLbl.Text;

                ModalOkAction = DeleteCategoryAction;

                ((Admin)this.Master).ShowMasterModal(
                    "Eliminar",
                    "Está seguro que desea eliminar la categoría?",
                    true
                );
            }
        }
    }
}
