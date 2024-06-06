using System;
using System.Collections.Generic;
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

        // CONSTRUCT

        public ProductView()
        {
            _product = new Product();
            _productsManager = new ProductsManager();
            _cartManager = new CartManager();
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

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();
            RequestOpenArticle();
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
