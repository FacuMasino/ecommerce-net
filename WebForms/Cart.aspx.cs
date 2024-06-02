using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms
{
    public partial class Cart : System.Web.UI.Page
    {
        // ATTRIBUTES

        private Product _product;
        private ProductsManager _productsManager;
        private CartManager _cartManager;

        // PROPERTIES

        public CartManager ProductsCart
        {
            get { return _cartManager; }
            set { _cartManager = value; }
        }

        // CONSTRUCT

        public Cart()
        {
            _productsManager = new ProductsManager();
            _cartManager = new CartManager();
        }

        // METHODS

        private void RequestAddedProduct()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                _product = _productsManager.Read(id);
                _cartManager.Add(_product);
                Session["CurrentProductSets"] = _cartManager.List();
            }
        }

        private void BindRepeater()
        {
            CartRepeater.DataSource = _cartManager.List();
            CartRepeater.DataBind();
        }

        private void BindControls()
        {
            //BindGridView();
            BindRepeater();
        }

        private void CheckSession()
        {
            if (Session["CurrentProductSets"] != null)
            {
                _cartManager.CurrentProductSets = (List<ProductSet>)Session["CurrentProductSets"];
            }
        }

        //EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckSession();

            if (!IsPostBack) // si es postback no bindear lista ni agregar article
            {
                RequestAddedProduct();
                BindControls();
            }
        }

        protected void removeLnkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            _cartManager.Remove(id);
            BindControls();
        }

        protected void addLnkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            _cartManager.Add(id);
            BindControls();
        }

        protected void deleteLnkButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((LinkButton)sender).CommandArgument);
            _cartManager.Delete(id);
            BindControls();
        }

        protected void CartRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (
                e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.AlternatingItem
            )
            {
                var dataItem = e.Item.DataItem as ProductSet;
                var articleImage = e.Item.FindControl("articleImage") as HtmlImage;

                if (dataItem != null)
                {
                    if (
                        dataItem.Images != null
                        && dataItem.Images.Count > 0
                        && dataItem.Images[0] != null
                    )
                    {
                        articleImage.Src = dataItem.Images[0].Url;
                    }
                    else
                    {
                        articleImage.Src = "Content/img/placeholder.jpg";
                    }

                    articleImage.Alt = $"Imagen de {dataItem.Name}";
                }
            }
        }
    }
}
