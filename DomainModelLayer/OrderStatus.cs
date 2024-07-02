using System.ComponentModel;

namespace DomainModelLayer
{
    public class OrderStatus
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Estado")]
        public string Name { get; set; }

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
