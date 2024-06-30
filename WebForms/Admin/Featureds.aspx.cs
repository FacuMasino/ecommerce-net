using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Featureds : System.Web.UI.Page
    {
        private ProductsManager _productsManager;
        private FeaturedsManager _featuredsManager;

        private List<FeaturedProduct> _featuredsList;

        int TotalFeatureds = 0;

        public Featureds()
        {
            _productsManager = new ProductsManager();
            _featuredsManager = new FeaturedsManager();
        }

        private void BindProductsList()
        {
            ProductListRepeater.DataSource = _productsManager.List(true, true); // Activos y con stock
            ProductListRepeater.DataBind();
        }

        private void BindFeaturedsList()
        {
            _featuredsList = new List<FeaturedProduct>();
            _featuredsList = _featuredsManager.List();

            Session["FeaturedsProducts"] = _featuredsList;

            FeaturedsListRepeater.DataSource = _featuredsList;
            FeaturedsListRepeater.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductsList();
                BindFeaturedsList();
            }
        }

        protected void AddProductLnkBtn_Click(object sender, EventArgs e)
        {
            int productId = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            Product auxProduct = _productsManager.Read(productId);
            FeaturedProduct auxFeatured = new FeaturedProduct(auxProduct)
            {
                DisplayOrder = ((List<FeaturedProduct>)Session["FeaturedsProducts"]).Count + 1,
                ShowAsNew = false // TODO: Tomar del checkbox
            };

            _featuredsManager.Add(auxFeatured);
        }

        protected void SearchBtn_Click(object sender, EventArgs e) { }

        protected void LevelUpLnkBtn_Click(object sender, EventArgs e) { }

        protected void LevelDownLnkBtn_Click(object sender, EventArgs e) { }

        protected void deleteFeaturedLnkBtn_Click(object sender, EventArgs e)
        {
            // TODO: Agregar confirmacion

            int productId = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            _featuredsManager.Delete(productId);
        }
    }
}
