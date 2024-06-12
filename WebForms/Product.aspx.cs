using System;
using System.Collections;
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
        private CartManager _cartManager;

        // PROPERTIES
        public List<Product> Products { get; set; }
        public List<Product> SugProducts { get; set; }

        // CONSTRUCT

        public ProductView()
        {
            _product = new Product();
            _productsManager = new ProductsManager();
            _cartManager = new CartManager();
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
            }
        }

        // Tenemos esto para obtener la cantidad de productos en Session
        // Para poder actualizar la cantidad en caso de modificarse
        // Desde los eventos Add y Remove
        private void CheckSession()
        {
            if (Session["CurrentProductSets"] != null)
            {
                _cartManager.CurrentProductSets = (List<ProductSet>)Session["CurrentProductSets"];
            }
        }

        /// <summary>
        /// Cantidad de producto en el carrito
        /// </summary>
        public int GetCartQty()
        {
            return _cartManager.Count(_product.Id);
        }

        protected void FilterBySuggestedList()
        {
            Products = _productsManager.List();
            int zise = Products.Count;

            if (zise > 5)
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

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();
            RequestOpenArticle();
            FilterBySuggestedList();
        }

        protected void RemoveLnkButton_Click(object sender, EventArgs e)
        {
            _cartManager.Remove(_product.Id);
            Session["CurrentProductSets"] = _cartManager.List();
        }

        protected void AddLnkButton_Click(object sender, EventArgs e)
        {
            _cartManager.Add(_product);
            Session["CurrentProductSets"] = _cartManager.List();
        }
    }
}
