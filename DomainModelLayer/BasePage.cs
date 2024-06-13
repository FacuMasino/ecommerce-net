namespace DomainModelLayer
{
    public class BasePage : System.Web.UI.Page
    {
        public virtual void OnModalConfirmed() { }

        public virtual void OnModalCancelled() { }
    }
}
