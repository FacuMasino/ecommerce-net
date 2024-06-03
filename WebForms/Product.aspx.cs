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
    public partial class Details : System.Web.UI.Page
    {
        public Product _product;
        ProductsManager _productsManager = new ProductsManager();

        // CONSTRUCT

        public Details()
        {
            _product = new Product();
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

        // EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            RequestOpenArticle();
        }
    }
}
