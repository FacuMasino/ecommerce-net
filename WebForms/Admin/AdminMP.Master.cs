using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DomainModelLayer;

namespace WebForms.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        // METHODS

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

        public void ShowMasterModal(string title, string message, bool requiresConfirm = false)
        {
            ResetMasterModal();
            MasterModalTitle.Text = title;
            MasterModalBody.Text = message;
            if (!requiresConfirm)
                MasterModalFrmChk.Visible = false;
            ScriptManager.RegisterStartupScript(
                Page,
                Page.GetType(),
                "masterModal",
                "masterModal.show()",
                true
            );
            MasterModalUP.Update();
        }

        public void ShowMasterToast(string message)
        {
            MasterToastBody.Text = message;
            ScriptManager.RegisterStartupScript(
                Page,
                Page.GetType(),
                "masterToast",
                "masterToast.show()",
                true
            );
            MasterToastUP.Update();
        }

        // Limpia los controles y clases css del Modal
        private void ResetMasterModal()
        {
            if (MasterModalChk.Checked)
                MasterModalChk.Checked = false; // Reset checkbox
            MasterModalBodyWrapper.Attributes["class"] = "d-flex flex-column"; // Reset invalid
        }

        //EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckActiveItem();
        }

        protected void MasterModalConfirmBtn_Click(object sender, EventArgs e)
        {
            if (MasterModalFrmChk.Visible && !MasterModalChk.Checked)
            {
                MasterModalBodyWrapper.Attributes["class"] = "d-flex flex-column invalid";
                return; // Si tenía que confirmar y no lo hizo, esperar confirmación
            }

            (BodyPlaceHolder.Page as BasePage).OnModalConfirmed(); // Disparar evento de confirmación

            ScriptManager.RegisterStartupScript( // Ocultar modal
                Page,
                Page.GetType(),
                "masterModal",
                "masterModal.hide()",
                true
            );
        }

        protected void MasterModalCancelBtn_Click(object sender, EventArgs e)
        {
            (BodyPlaceHolder.Page as BasePage).OnModalCancelled();
        }
    }
}
