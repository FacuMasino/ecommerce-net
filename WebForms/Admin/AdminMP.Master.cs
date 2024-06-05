using System;
using System.Web.UI.HtmlControls;

namespace WebForms.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckActiveItem();
        }

        private void CheckActiveItem()
        {
            switch (Request.Path)
            {
                case "/admin/Dashboard.aspx":
                    SetActiveItem(BtnNavHome);
                    break;
                case "/admin/Products.aspx":
                    SetActiveItem(BtnNavProducts);
                    break;
                case "/admin/Product.aspx":
                    SetActiveItem(BtnNavProducts);
                    break;
                case "/admin/Categories.aspx":
                    SetActiveItem(BtnNavCategories);
                    break;
                case "/admin/Orders.aspx":
                    SetActiveItem(BtnNavOrders);
                    break;
                case "/admin/Users.aspx":
                    SetActiveItem(BtnNavUsers);
                    break;
                default:
                    break;
            }
        }

        private void SetActiveItem(HtmlAnchor item)
        {
            string prevClasses = BtnNavHome.Attributes["class"];
            item.Attributes["class"] = prevClasses + " active";
        }
    }
}
