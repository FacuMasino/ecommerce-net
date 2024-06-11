using System;
using System.Collections.Generic;
using DataAccessLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace BusinessLogicLayer
{
    public class ProductsManager
    {
        private DataAccess _dataAccess = new DataAccess();
        private BrandsManager _brandsManager = new BrandsManager();
        private CategoriesManager _categoriesManager = new CategoriesManager();
        private ImagesManager _imagesManager = new ImagesManager();

        public List<Product> List()
        {
            List<Product> products = new List<Product>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Products");
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Product product = new Product();
                    product.Id = (int)_dataAccess.Reader["ProductId"];
                    product.Code = _dataAccess.Reader["Code"]?.ToString();
                    product.Name = _dataAccess.Reader["ProductName"]?.ToString();
                    product.Description = _dataAccess.Reader["ProductDescription"]?.ToString();
                    product.Price = _dataAccess.Reader["Price"] as decimal? ?? product.Price;
                    product.Cost = _dataAccess.Reader["Cost"] as decimal? ?? product.Cost;
                    product.Stock = (int)_dataAccess.Reader["Stock"] as int? ?? product.Stock;
                    product.Brand.Id = _dataAccess.Reader["BrandId"] as int? ?? product.Brand.Id;
                    product.Categories = _categoriesManager.List(product.Id);
                    product.Images = _imagesManager.List(product.Id);
                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            foreach (Product product in products)
            {
                product.Brand = _brandsManager.Read(product.Brand.Id);
            }

            return products;
        }

        public Product Read(int productId)
        {
            Product product = new Product();

            try
            {
                _dataAccess.SetQuery(
                    "select Code, ProductName, ProductDescription, Price, Cost, Stock, BrandId from Products where ProductId = @ProductId"
                );
                _dataAccess.SetParameter("@ProductId", productId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    product.Id = productId;
                    product.Code = _dataAccess.Reader["Code"]?.ToString();
                    product.Name = _dataAccess.Reader["ProductName"]?.ToString();
                    product.Description = _dataAccess.Reader["ProductDescription"]?.ToString();
                    product.Price = _dataAccess.Reader["Price"] as decimal? ?? product.Price;
                    product.Cost = _dataAccess.Reader["Cost"] as decimal? ?? product.Cost;
                    product.Stock = (int)_dataAccess.Reader["Stock"] as int? ?? product.Stock;
                    product.Brand.Id = _dataAccess.Reader["BrandId"] as int? ?? product.Brand.Id;
                    product.Categories = _categoriesManager.List(product.Id);
                    product.Images = _imagesManager.List(product.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            product.Brand = _brandsManager.Read(product.Brand.Id);

            return product;
        }

        public void Add(Product Product)
        {
            SetBrandId(Product);

            try
            {
                _dataAccess.SetQuery(
                    "insert into Products (Code, ProductName, ProductDescription, Price, Cost, BrandId) values (@Code, @ProductName, @ProductDescription, @Price, @Cost, @BrandId)"
                );
                SetParameters(Product);
                _dataAccess.ExecuteAction();
                SetImages(Product); // Las imagenes se agregan luego de agregar el articulo ya que van con le id del mismo asociadas
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public void Edit(Product product)
        {
            SetBrandId(product);
            SetImages(product);

            try
            {
                _dataAccess.SetQuery(
                    "update Products set Code = @Code, ProductName = @ProductName, ProductDescription = @ProductDescription, Price = @Price, Cost = @Cost, BrandId = @BrandId where ProductId = @ProductId"
                );
                _dataAccess.SetParameter("@ProductId", product.Id);
                SetParameters(product);
                _dataAccess.ExecuteAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        public void Delete(Product product, bool purgeBrand = false, bool purgeCategory = false)
        {
            try
            {
                _dataAccess.SetQuery("delete from Products where ProductId = @ProductId");
                _dataAccess.SetParameter("@ProductId", product.Id);
                _dataAccess.ExecuteAction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            if (purgeBrand)
            {
                _brandsManager.PurgeBrand(product.Brand);
            }

            if (purgeCategory)
            {
                foreach (Category category in product.Categories)
                {
                    _categoriesManager.PurgeCategory(category);
                }
            }
        }

        public int GetId(Product product)
        {
            if (product == null)
            {
                return 0;
            }

            int id = 0;

            try
            {
                _dataAccess.SetQuery("select ProductId from Products where Code = @Code");
                _dataAccess.SetParameter("@Code", product.Code);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    id = (int)_dataAccess.Reader["ProductId"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            return id;
        }

        private void SetParameters(Product product)
        {
            _dataAccess.SetParameter("@Code", product.Code);
            _dataAccess.SetParameter("@ProductName", product.Name);
            _dataAccess.SetParameter("@ProductDescription", product.Description);
            _dataAccess.SetParameter("@Price", product.Price);
            _dataAccess.SetParameter("@Cost", product.Cost);
            _dataAccess.SetParameter("@BrandId", product.Brand?.Id);
        }

        private void SetBrandId(Product product)
        {
            if (product.Brand != null)
            {
                int dbBrandId = _brandsManager.GetId(product.Brand);

                if (dbBrandId == 0)
                {
                    _brandsManager.Add(product.Brand);
                    product.Brand.Id = Helper.GetLastId("Brands");
                }
                else
                {
                    product.Brand.Id = dbBrandId;
                }
            }
        }

        private void SetImages(Product product)
        {
            int productId = product.Id == 0 ? Helper.GetLastId("Products") : product.Id; // si es un articulo nuevo, se obtiene el id nuevo

            foreach (var image in product.Images)
            {
                if (image != null)
                {
                    if (image.Id == 0) // si es una imagen nueva, se agrega y obtiene id
                    {
                        _imagesManager.Add(image, productId);
                        image.Id = Helper.GetLastId("Images");
                    }
                    else // sino se edita solamente
                    {
                        _imagesManager.Edit(image);
                    }
                }
            }
        }
    }
}
