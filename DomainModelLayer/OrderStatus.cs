using System.ComponentModel;

namespace DomainModelLayer
{
    public class OrderStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TransitionText { get; set; }

        public string AcceptedText { get; set; }

        public override string ToString()
        {
            if (Name != null)
            {
                return Name;
            }
            else
            {
                return "";
            }
        }
    }
}
