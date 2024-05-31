using System;
using System.ComponentModel;

namespace DomainModelLayer
{
    internal class Order
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Usuario")]
        public User User { get; set; }

        [DisplayName("Fecha de creación")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Fecha de entrega")]
        public DateTime DeliveryDate { get; set; }

        [DisplayName("Domicilio de entrega")]
        public Adress DeliveryAdress { get; set; }

        [DisplayName("Estado")]
        public OrderStatus Status { get; set; }
    }
}
