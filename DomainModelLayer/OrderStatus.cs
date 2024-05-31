using System.ComponentModel;

namespace DomainModelLayer
{
    internal class OrderStatus
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Estado")]
        public string Name { get; set; }
    }
}
