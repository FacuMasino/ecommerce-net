using System.ComponentModel;

namespace DomainModelLayer
{
    public class OrderStatus
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Estado")]
        public string Name { get; set; }
    }
}
