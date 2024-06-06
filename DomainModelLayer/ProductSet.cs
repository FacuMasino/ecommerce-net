namespace DomainModelLayer
{
    public class ProductSet : Product
    {
        public int Quantity { get; set; }

        public decimal Subtotal
        {
            get { return Quantity * Price; }
        }
    }
}
