using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DomainModelLayer;

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

        public void ShowMasterModal(string title, string message)
        {
            MasterModalTitle.Text = title;
            MasterModalBody.Text = message;
            ScriptManager.RegisterStartupScript(
                Page,
                Page.GetType(),
                "MasterModal",
                "MasterModal.show()",
                true
            );
            MasterModalUP.Update();
        }

        protected void MasterModalConfirmBtn_Click(object sender, EventArgs e)
        {
            //Session["MasterModalConfirm"] = true;
            (BodyPlaceHolder.Page as BasePage).OnModalConfirmed();
        }

        protected void MasterModalCancelBtn_Click(object sender, EventArgs e)
        {
            //Session["MasterModalConfirm"] = false;
            (BodyPlaceHolder.Page as BasePage).OnModalCancelled();
        }
    }
}
