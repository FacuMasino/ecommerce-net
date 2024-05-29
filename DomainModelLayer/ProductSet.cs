namespace DomainModelLayer
{
    public class ProductSet : Product
    {
        public int Amount { get; set; }

        public decimal Subtotal
        {
            get { return Amount * Price; }
        }
    }
}
