using System;
using System.Collections.Generic;
using System.Linq;
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

        public int TotalProducts;
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; }
        public List<Brand> Brands { get; }
        public List<Product> FeaturedProducts { get; set; }

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
            if (id == -1) // Productos sin categoría
            {
                Products = Products.FindAll(a => a.Categories.Count < 1);
                return;
            }

            Products = Products.Where(p => p.Categories.Any(c => c.Id == id)).ToList();
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

        private void GetFeaturedProducts()
        {
            // Aca se deberia obtener la lista de productos destacados
            // Si tuviera 0 productos entonces podrian usarse los ultimos agregados
            // y mostrarlos como "nuevos ingresos"

            // Por ahora solo se van a usar los 4 primeros de la lista normal
            FeaturedProducts = new List<Product>();
            FeaturedProducts = Products.Take(4).ToList();
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            GetFeaturedProducts();
            CheckRequest(); // Verificar si se pasaron parámetros
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            string filter = searchTextBox.Text;

            if (2 < filter.Length)
            {
                searchPanel.CssClass = "input-group mb-3";
                Products = Products.FindAll(
                    x =>
                        x.Name.ToUpper().Contains(filter.ToUpper())
                        || x.Brand.ToString().ToUpper().Contains(filter.ToUpper())
                        || x.Code.ToUpper().Contains(filter.ToUpper())
                        || x.Description.ToUpper().Contains(filter.ToUpper())
                        || x.Categories.Any(c => c.Name.ToUpper().Contains(filter.ToUpper()))
                );
            }
            else
            {
                searchPanel.CssClass = "input-group mb-3 invalid";
            }
        }
    }
}
