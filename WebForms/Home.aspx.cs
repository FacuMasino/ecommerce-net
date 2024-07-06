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
        private FeaturedsManager _featuredsManager;
        private bool _isSearching;
        private string _textToSearch;

        // PROPERTIES

        public int TotalProducts;
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; }
        public List<Brand> Brands { get; }
        public List<FeaturedProduct> Featureds { get; set; }

        // CONSTRUCT

        public Home()
        {
            _productsManager = new ProductsManager();
            _categoriesManager = new CategoriesManager();
            _brandsManager = new BrandsManager();
            _featuredsManager = new FeaturedsManager();
            Products = _productsManager.List<Product>(true, true); // Solo activos y en stock
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
            Featureds = _featuredsManager.List();
            if (Featureds.Count == 0)
            {
                GenerateFeaturedProducts();
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

        private void GetProductsList()
        {
            Products = _productsManager.List<Product>(true, true);
        }

        /// <summary>
        /// Genera una lista con los ultimos 3 productos agregados
        ///  mostrandolos como Nuevos Ingresos
        /// </summary>
        private void GenerateFeaturedProducts()
        {
            if (Products.Count <= 3)
                return;

            for (int i = 0; i < 3; i++)
            {
                Featureds.Add(
                    new FeaturedProduct(Products[Products.Count - (i + 1)])
                    {
                        ShowAsNew = true,
                        DisplayOrder = i
                    }
                );
            }
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            GetFeaturedProducts();
            CheckRequest(); // Verificar si se pasaron parámetros
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

                GetProductsList();
                return;
            }

            if (2 <= filter.Length)
            {
                SearchPanel.CssClass = "input-group mb-3";
                Products = Products.FindAll(
                    x =>
                        x.Name.ToUpper().Contains(filter.ToUpper())
                        || x.Brand.ToString().ToUpper().Contains(filter.ToUpper())
                        || x.Code.ToUpper().Contains(filter.ToUpper())
                        || x.Description.ToUpper().Contains(filter.ToUpper())
                        || x.Categories.Any(c => c.Name.ToUpper().Contains(filter.ToUpper()))
                );
                SearchBtn.Text = "<i class=\"bi bi-x-circle\"></i>"; // cambia icono boton de busqueda
            }
            else
            {
                SearchPanel.CssClass = "input-group mb-3 invalid";
            }

            SetSearchState(true, filter); // Guarda el estado para saber que está buscando
        }
    }
}
