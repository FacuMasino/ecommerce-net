using System.Collections.Generic;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class CartManager
    {
        // ATTRIBUTES

        private Cart _cart;
        private ProductSet _ProductSet;

        // PROPERTIES

        public List<ProductSet> CurrentProductSets
        {
            set { _cart.ProductSets = value; }
        }

        // CONSTRUCT

        public CartManager()
        {
            _cart = new Cart();
        }

        // METHODS

        public List<ProductSet> List()
        {
            return _cart.ProductSets;
        }

        private bool ProductExists(int id)
        {
            if (_cart.ProductSets.Find(x => x.Id == id) == null)
            {
                return false;
            }

            return true;
        }

        private ProductSet ReadProductSet(int id) // Me tomé el atrevimiento de cambiar el nombre de tu método Facu <3
        {
            return _cart.ProductSets.Find(x => x.Id == id);
        }

        public void Add(Product Product, int amount = 1)
        {
            if (ProductExists(Product.Id))
            {
                Add(Product.Id);
            }
            else
            {
                _ProductSet = new ProductSet
                {
                    Id = Product.Id,
                    Code = Product.Code,
                    Name = Product.Name,
                    Price = Product.Price,
                    Description = Product.Description,
                    Brand = Product.Brand,
                    Category = Product.Category,
                    Images = Product.Images,
                    Amount = amount
                };

                _cart.ProductSets.Add(_ProductSet);
            }
        }

        public void Add(int ProductId, int amount = 1)
        {
            _ProductSet = ReadProductSet(ProductId);
            _ProductSet.Amount += amount;
        }

        public void Remove(int ProductId, int amount = 1)
        {
            _ProductSet = ReadProductSet(ProductId);

            if (_ProductSet.Amount != amount)
            {
                _ProductSet.Amount -= amount;
                return;
            }

            Delete(ProductId);
        }

        public void Delete(int ProductId)
        {
            _ProductSet = ReadProductSet(ProductId);
            _cart.ProductSets.Remove(_ProductSet);
        }

        public void Clear()
        {
            _cart.ProductSets.Clear();
        }

        public int Count()
        {
            return _cart.ProductSets.Count;
        }

        public decimal GetTotal()
        {
            return _cart.Total;
        }
    }
}
