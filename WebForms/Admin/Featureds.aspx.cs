using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace WebForms.Admin
{
    public partial class Featureds : BasePage
    {
        private ProductsManager _productsManager;
        private FeaturedsManager _featuredsManager;
        private List<FeaturedProduct> _featuredsList;
        private List<Product> _productsList;
        private Admin _adminMP; // Referencia a la instancia de la Admin Master Page
        private int _currentProductId;

        public int TotalFeatureds
        {
            get
            {
                if (Session["FeaturedsProducts"] != null)
                {
                    return ((List<FeaturedProduct>)Session["FeaturedsProducts"]).Count;
                }
                else
                {
                    return _featuredsList.Count;
                }
            }
        }

        public Featureds()
        {
            _productsManager = new ProductsManager();
            _featuredsManager = new FeaturedsManager();
            _featuredsList = new List<FeaturedProduct>();
            _productsList = new List<Product>();
        }

        /// <summary>
        /// Excluye de la lista los que ya estan en destacados
        /// </summary>
        /// <param name="productsList">Lista de productos</param>
        /// <returns>Lista filtrada</returns>
        private List<Product> FilterProductsList(List<Product> productsList)
        {
            productsList = productsList.Where(p => !_featuredsList.Any(f => f.Id == p.Id)).ToList();

            return productsList;
        }

        private void UpdateProductsList(bool isSearching = false)
        {
            if (!isSearching)
            {
                GetProductsList();
            }
            ProductListRepeater.DataSource = FilterProductsList(_productsList); // Activos y con stock
            ProductListRepeater.DataBind();
        }

        private void UpdateProductsList(MasterPage master)
        {
            GetProductsList();
            Repeater auxRpt = (Repeater)Helper.FindControl(master, "ProductListRepeater");
            auxRpt.DataSource = FilterProductsList(_productsList);
            auxRpt.DataBind();
        }

        private void GetFeaturedsList()
        {
            Session.Remove("FeaturedsProducts"); // Limpiar sesion
            _featuredsList = _featuredsManager.List();
            Session["FeaturedsProducts"] = _featuredsList;
        }

        private void GetProductsList()
        {
            Session.Remove("Products");
            _productsList = _productsManager.List(true, true);
            Session["Products"] = _productsList;
        }

        private void UpdateFeaturedsList()
        {
            GetFeaturedsList();
            FeaturedsListRepeater.DataSource = _featuredsList;
            FeaturedsListRepeater.DataBind();
        }

        private void UpdateFeaturedsList(MasterPage master)
        {
            GetFeaturedsList();
            Repeater auxRpt = (Repeater)Helper.FindControl(master, "FeaturedsListRepeater");
            auxRpt.DataSource = _featuredsList;
            auxRpt.DataBind();
        }

        private void CheckSession()
        {
            if (Session["FeaturedsProducts"] != null)
            {
                _featuredsList = (List<FeaturedProduct>)Session["FeaturedsProducts"];
            }
            if (Session["Products"] != null)
            {
                _productsList = (List<Product>)Session["Products"];
            }
        }

        private void Notify(string message, MasterPage master)
        {
            Admin adminMP = (Admin)master;
            adminMP.ShowMasterToast(message);
        }

        private void Notify(string message)
        {
            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterToast(message);
        }

        private void RemoveFeaturedAction(MasterPage master)
        {
            _featuredsManager.Delete(_currentProductId);
            Notify("El producto se quitó de la lista de destacados.", master);
            UpdateFeaturedsList(master);
            UpdateProductsList(master);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _adminMP = (Admin)this.Master; // Se asigna la instancia de la Admin Master Page
            // Para luego poder acceder a sus metodos

            if (IsPostBack)
            {
                CheckSession();
            }

            if (!IsPostBack)
            {
                UpdateFeaturedsList();
                UpdateProductsList();
            }
        }

        protected void AddProductLnkBtn_Click(object sender, EventArgs e)
        {
            if (_featuredsList.Count == 6)
            {
                Notify(
                    "Solo es posible agregar hasta 6 destacados. Quite alguno de la lista para añadir otro."
                );
                return;
            }
            int productId = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            Product auxProduct = _productsManager.Read(productId);
            FeaturedProduct auxFeatured = new FeaturedProduct(auxProduct)
            {
                DisplayOrder = _featuredsList.Count,
                ShowAsNew = false
            };

            _featuredsManager.Add(auxFeatured);
            UpdateFeaturedsList();
            UpdateProductsList();
            SearchTextBox.Text = ""; // Si estaba buscando, se resetea el campo
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string filter = SearchTextBox.Text;

            if (2 < filter.Length)
            {
                SearchPanel.CssClass = "input-group mb-3";
                _productsList = _productsList.FindAll(
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
                SearchPanel.CssClass = "input-group mb-3 invalid";
            }

            UpdateProductsList(true);
            ProductsCollapse.CssClass = "collapse border rounded-bottom p-3 show";
        }

        protected void LevelUpLnkBtn_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            FeaturedProduct product = _featuredsList.Find(p => p.Id == productId);

            // Con el indice del producto que se quiere subir de nivel
            // se busca al producto anterior para poder pasar el Id a la funcion
            // del manager y modificarlo
            int currentProductIndex = _featuredsList.IndexOf(product);
            int previousProductId = _featuredsList[currentProductIndex - 1].Id;
            _featuredsManager.LevelUpProduct(product, previousProductId);
            UpdateFeaturedsList();
        }

        protected void LevelDownLnkBtn_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            FeaturedProduct product = _featuredsList.Find(p => p.Id == productId);

            // Con el indice del producto que se quiere bajar de nivel
            // se busca al producto siguiente para poder pasar el Id a la funcion
            // del manager y modificarlo
            int currentProductIndex = _featuredsList.IndexOf(product);
            int nextProductId = _featuredsList[currentProductIndex + 1].Id;
            _featuredsManager.LevelDownProduct(product, nextProductId);
            UpdateFeaturedsList();
        }

        protected void removeFeaturedLnkBtn_Click(object sender, EventArgs e)
        {
            ModalOkAction = RemoveFeaturedAction; // Si luego confirma se llama a esta función

            _currentProductId = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            _adminMP.ShowMasterModal(
                "Quitar Destacado",
                "Está seguro que desea quitar el producto de Destacados?",
                true
            );
        }

        protected void newProductChk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAux = (CheckBox)sender;
            int productId = Convert.ToInt32(chkAux.Attributes["CommandName"]);
            FeaturedProduct product = _featuredsList.Find(p => p.Id == productId);
            _featuredsManager.SetShowAsNew(productId, !product.ShowAsNew);
            UpdateFeaturedsList();
        }
    }
}
