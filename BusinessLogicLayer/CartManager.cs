using System.Collections.Generic;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class CartManager
    {
        // ATTRIBUTES

        private ShoppingCart _shoppingCart;
        private ProductSet _ProductSet;

        // PROPERTIES

        public List<ProductSet> CurrentProductSets
        {
            set { _shoppingCart.ProductSets = value; }
        }

        // CONSTRUCT

        public CartManager()
        {
            _shoppingCart = new ShoppingCart();
        }

        // METHODS

        public List<ProductSet> List()
        {
            return _shoppingCart.ProductSets;
        }

        private bool ProductExists(int id)
        {
            if (_shoppingCart.ProductSets.Find(x => x.Id == id) == null)
            {
                return false;
            }

            return true;
        }

        private ProductSet ReadProductSet(int id)
        {
            return _shoppingCart.ProductSets.Find(x => x.Id == id);
        }

        public void Add(Product Product, int quantity = 1)
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
                    Images = Product.Images,
                    Quantity = quantity
                };

                _shoppingCart.ProductSets.Add(_ProductSet);
            }
        }

        public void Add(int ProductId, int quantity = 1)
        {
            _ProductSet = ReadProductSet(ProductId);
            _ProductSet.Quantity += quantity;
        }

        public void Remove(int ProductId, int quantity = 1)
        {
            _ProductSet = ReadProductSet(ProductId);

            if (_ProductSet.Quantity != quantity)
            {
                _ProductSet.Quantity -= quantity;
                return;
            }

            Delete(ProductId);
        }

        public void Delete(int ProductId)
        {
            _ProductSet = ReadProductSet(ProductId);
            _shoppingCart.ProductSets.Remove(_ProductSet);
        }

        public void Clear()
        {
            _shoppingCart.ProductSets.Clear();
        }

        /// <summary>
        /// Cuenta el total de productos en el carrito
        /// </summary>
        /// <returns>Cantidad de productos</returns>
        public int Count()
        {
            return _shoppingCart.ProductSets.Count;
        }

        /// <summary>
        /// Cuenta la cantidad de un producto en el carrito
        /// según el Id pasado como parámetro
        /// </summary>
        /// <returns>Cantidad del producto</returns>
        public int Count(int productId)
        {
            int count = 0;
            if (_shoppingCart.ProductSets.Count > 0)
            {
                // Si .Find no devuelve null, intenta acceder a la prop Quantiy
                // Sino se asigna 0
                count = _shoppingCart.ProductSets.Find(p => p.Id == productId)?.Quantity ?? 0;
            }
            return count;
        }

        public decimal GetTotal()
        {
            return _shoppingCart.Total;
        }
    }
}
