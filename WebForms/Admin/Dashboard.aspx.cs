using System;
using System.Collections.Generic;
using BusinessLogicLayer;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        StatsManager _statsManager = new StatsManager();
        private List<ProductStats> _topSellingProducts = new List<ProductStats>();
        private List<ProductStats> _topVisitedProducts = new List<ProductStats>();

        public int FinishedOrders { get; set; }
        public int ActiveProducts { get; set; }
        public int SoldProducts { get; set; }
        public int ShippedProducts { get; set; }
        public int TotalTopSelling
        {
            get { return _topSellingProducts == null ? 0 : _topSellingProducts.Count; }
        }

        public int TotalTopVisited
        {
            get { return _topVisitedProducts == null ? 0 : _topVisitedProducts.Count; }
        }

        private void Notify(string message)
        {
            Admin adminMP = (Admin)this.Master;
            adminMP.ShowMasterToast(message);
        }

        private void BindTopSellingList()
        {
            TopSellingProductsRpt.DataSource = _topSellingProducts;
            TopSellingProductsRpt.DataBind();
        }

        private void BindTopVisitedList()
        {
            TopVisitedProductsRpt.DataSource = _topVisitedProducts;
            TopVisitedProductsRpt.DataBind();
        }

        private void LoadStats()
        {
            try
            {
                FinishedOrders = _statsManager.CountFinishedOrders();
                ActiveProducts = _statsManager.CountActiveProducts();
                SoldProducts = _statsManager.CountSoldProducts();
                ShippedProducts = _statsManager.CountShippedProducts();
                _topSellingProducts = _statsManager.TopSellingProducts();
                _topVisitedProducts = _statsManager.TopVisitedProducts();
            }
            catch (Exception ex)
            {
                Notify($"Ocurrió un error: <br/>{ex.Message}");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadStats();
            BindTopSellingList();
            BindTopVisitedList();
        }
    }
}
