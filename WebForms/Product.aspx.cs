using System;
using System.Collections.Generic;
using System.Diagnostics;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Details : System.Web.UI.Page
    {
        // ATTRIBUTES

        public Product _product;
        private ProductsManager _productsManager;
        private CartManager _cartManager;

        // PROPERTIES

        // CONSTRUCT

        public Details()
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

        protected void RemoveLnkButton_Click(object sender, EventArgs e) { }

        protected void AddLnkButton_Click(object sender, EventArgs e) { }
    }
}
