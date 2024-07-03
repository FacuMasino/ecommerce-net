using System.Collections.Generic;

namespace DomainModelLayer
{
    public class DistributionChannel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<OrderStatus> OrderStatuses { get; set; }

        public DistributionChannel()
        {
            OrderStatuses = new List<OrderStatus>();
        }

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
