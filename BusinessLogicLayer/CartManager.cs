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

        /// <summary>
        /// Cuenta el total de productos en el carrito
        /// </summary>
        /// <returns>Cantidad de productos</returns>
        public int Count()
        {
            return _cart.ProductSets.Count;
        }

        /// <summary>
        /// Cuenta la cantidad de un producto en el carrito
        /// según el Id pasado como parámetro
        /// </summary>
        /// <returns>Cantidad del producto</returns>
        public int Count(int productId)
        {
            int count = 0;
            if (_cart.ProductSets.Count > 0)
            {
                // Si .Find no devuelve null, intenta acceder a la prop Amount
                // Sino se asigna 0
                count = _cart.ProductSets.Find(p => p.Id == productId)?.Amount ?? 0;
            }
            return count;
        }

        public decimal GetTotal()
        {
            return _cart.Total;
        }
    }
}
