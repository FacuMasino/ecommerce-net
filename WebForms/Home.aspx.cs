using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Home : System.Web.UI.Page
    {
        // ATTRIBUTES

        private ProductsManager _productsManager;
        private CategoriesManager _categoriesManager;
        private BrandsManager _brandsManager;

        // PROPERTIES

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; }
        public List<Brand> Brands { get; }
        public int TotalProducts;

        // CONSTRUCT

        public Home()
        {
            _productsManager = new ProductsManager();
            _categoriesManager = new CategoriesManager();
            _brandsManager = new BrandsManager();
            Products = _productsManager.List();
            Categories = _categoriesManager.List();
            Brands = _brandsManager.List();
            TotalProducts = Products.Count; // Total de productos que permanece original
        }

        // METHODS

        private void FilterProductsByCategory(int id)
        {
            /*
            if (id == -1) //Productos sin categoría
            {
                Products = Products.FindAll(a => a.Category.Name == "");
                return;
            }
            Products = Products.FindAll(a => a.Category.Id == id);
            */ // hack
        }

        private void FilterProductsByBrand(int id)
        {
            if (id == -1) // Productos sin marca
            {
                Products = Products.FindAll(a => a.Brand.Name == "");
                return;
            }
            Products = Products.FindAll(a => a.Brand.Id == id);
        }

        private void CheckRequest()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["catId"]))
            {
                int categoryId = Convert.ToInt32(Request.QueryString["catId"].ToString());
                FilterProductsByCategory(categoryId);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["brandId"]))
            {
                int brandId = Convert.ToInt32(Request.QueryString["brandId"].ToString());
                FilterProductsByBrand(brandId);
            }
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckRequest(); // Verificar si se pasaron parámetros
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            string filter = searchTextBox.Text;

            if (2 < filter.Length)
            {
                searchPanel.CssClass = "input-group mb-3";
                Products = Products.FindAll(x =>
                    x.Name.ToUpper().Contains(filter.ToUpper())
                    || x.Brand.ToString().ToUpper().Contains(filter.ToUpper())
                    || x.Code.ToUpper().Contains(filter.ToUpper())
                    || x.Description.ToUpper().Contains(filter.ToUpper())
                    //|| x.Category.ToString().ToUpper().Contains(filter.ToUpper()) // hack
                );
            }
            else
            {
                searchPanel.CssClass = "input-group mb-3 invalid";
            }
        }
    }
}
