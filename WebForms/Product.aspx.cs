using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class ProductView : System.Web.UI.Page
    {
        // ATTRIBUTES

        public Product _product;
        private ProductsManager _productsManager;
        private CartManager _shoppingCartManager;

        // PROPERTIES
        public List<Product> Products { get; set; }
        public List<Product> SugProducts { get; set; }

        // CONSTRUCT

        public ProductView()
        {
            _product = new Product();
            _productsManager = new ProductsManager();
            _shoppingCartManager = new CartManager();
            Products = new List<Product>();
            SugProducts = new List<Product>();
        }

        // METHODS

        private void RequestOpenArticle()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int articleId = Convert.ToInt32(Request.QueryString["Id"].ToString());
                _product = _productsManager.Read(articleId);
                if (_product.Id != 0) // Sumar 1 visita
                    _productsManager.AddVisit(articleId);
            }
        }

        // Tenemos esto para obtener la cantidad de productos en Session
        // Para poder actualizar la cantidad en caso de modificarse
        // Desde los eventos Add y Remove
        private void CheckSession()
        {
            if (Session["CurrentProductSets"] != null)
            {
                _shoppingCartManager.CurrentProductSets =
                    (List<ProductSet>)Session["CurrentProductSets"];
            }
        }

        /// <summary>
        /// Cantidad de producto en el carrito
        /// </summary>
        public int GetCartQty()
        {
            return _shoppingCartManager.Count(_product.Id);
        }

        protected void FilterBySuggestedList()
        {
            Products = _productsManager.List<Product>();
            Products = Products.Where(p => p.Categories.Any(c => c.Id == _product.Id)).ToList();

            int size = Products.Count;

            if (size > 5)
            {
                SugProducts = Products.GetRange(0, 5);
                SuggestedRepeater.DataSource = SugProducts;
                SuggestedRepeater.DataBind();
            }
            else
            {
                SuggestedRepeater.DataSource = Products;
                SuggestedRepeater.DataBind();
            }
        }

        public bool IsOnCart()
        {
            List<ProductSet> auxProductSets = (List<ProductSet>)Session["CurrentProductSets"];
            if (auxProductSets == null)
                return false;
            if (auxProductSets.Find(p => p.Id == _product.Id) == null)
            {
                return false;
            }
            return true;
        }

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();
            RequestOpenArticle();
            FilterBySuggestedList();

            if (_product != null)
                this.Title = $"{_product.Brand} - {_product.Name}";
        }

        protected void RemoveLnkButton_Click(object sender, EventArgs e)
        {
            if (!IsOnCart())
                return;
            _shoppingCartManager.Remove(_product.Id);
            Session["CurrentProductSets"] = _shoppingCartManager.List();
        }

        protected void AddLnkButton_Click(object sender, EventArgs e)
        {
            _shoppingCartManager.Add(_product);
            Session["CurrentProductSets"] = _shoppingCartManager.List();
        }
    }
}
