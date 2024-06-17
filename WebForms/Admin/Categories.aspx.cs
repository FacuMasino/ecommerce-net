using System;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Categories : System.Web.UI.Page
    {
        // ATTRIBUTES

        private Category _category;
        private CategoriesManager _categoriesManager;

        // CONSTRUCT

        public Categories()
        {
            _category = new Category();
            _categoriesManager = new CategoriesManager();
        }

        // METHODS

        private void BindCategoriesRpt()
        {
            CategoriesListRpt.DataSource = _categoriesManager.List();
            CategoriesListRpt.DataBind();
        }

        private void ToggleEditMode(RepeaterItem item, bool isEditMode)
        {
            Label nameLabel = (Label)item.FindControl("CategoryNameLabel");
            TextBox editTextBox = (TextBox)item.FindControl("EditCategoryNameTxt");
            LinkButton editBtn = (LinkButton)item.FindControl("EditCategoryBtn");
            LinkButton saveBtn = (LinkButton)item.FindControl("SaveCategoryBtn");
            LinkButton cancelBtn = (LinkButton)item.FindControl("CancelEditBtn");

            nameLabel.Visible = !isEditMode;
            editTextBox.CssClass = isEditMode ? "form-control" : "form-control d-none";
            editBtn.Visible = !isEditMode;
            saveBtn.CssClass = isEditMode ? "p-0 text-black fs-5" : "p-0 text-black fs-5 d-none";
            cancelBtn.CssClass = isEditMode ? "p-0 text-black fs-5" : "p-0 text-black fs-5 d-none";
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoriesRpt();
            }
        }

        protected void DeleteCategoryBtn_Click(object sender, EventArgs e)
        {

        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

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
                TextBox editTextBox = (TextBox)e.Item.FindControl("EditCategoryNameTxt");
                _category.Id = Convert.ToInt32(e.CommandArgument);
                _category.Name = editTextBox.Text;
                _categoriesManager.Edit(_category);
                ToggleEditMode(e.Item, false);
                BindCategoriesRpt();
            }
            else if (e.CommandName == "Cancel")
            {
                ToggleEditMode(e.Item, false);
            }
        }
    }
}