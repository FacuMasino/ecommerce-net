using System.Collections.Generic;
using System.Linq;

namespace DomainModelLayer
{
    public class Cart
    {
        // PROPERTIES

        public List<ProductSet> ProductSets { get; set; }

        public decimal Total
        {
            get
            {
                return ProductSets.Sum<ProductSet>(set => set.Subtotal);
            }
        }

        // CONSTRUCT

        public Cart()
        {
            ProductSets = new List<ProductSet>();
        }
    }
}
