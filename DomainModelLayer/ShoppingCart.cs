using System.Collections.Generic;
using System.Linq;

namespace DomainModelLayer
{
    public class ShoppingCart
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

        public ShoppingCart()
        {
            ProductSets = new List<ProductSet>();
        }
    }
}
