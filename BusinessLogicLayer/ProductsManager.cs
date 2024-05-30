using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    product.Brand.Id = _dataAccess.Reader["BrandId"] as int? ?? product.Brand.Id;
                    product.Category.Id = _dataAccess.Reader["CategoryId"] as int? ?? product.Category.Id;
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
                product.Category = _categoriesManager.Read(product.Category.Id);
            }

            return products;
        }

        public Product Read(int productId)
        {
            Product Product = new Product();

            try
            {
                _dataAccess.SetQuery("select Code, ProductName, ProductDescription, Price, BrandId, CategoryId from Products where ProductId = @ProductId");
                _dataAccess.SetParameter("@ProductId", productId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    Product.Id = productId;
                    Product.Code = _dataAccess.Reader["Code"]?.ToString();
                    Product.Name = _dataAccess.Reader["ProductName"]?.ToString();
                    Product.Description = _dataAccess.Reader["ProductDescription"]?.ToString();
                    Product.Price = _dataAccess.Reader["Price"] as decimal? ?? Product.Price;
                    Product.Brand.Id = _dataAccess.Reader["BrandId"] as int? ?? Product.Brand.Id;
                    Product.Category.Id = _dataAccess.Reader["CategoryId"] as int? ?? Product.Category.Id;
                    Product.Images = _imagesManager.List(Product.Id);
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

            Product.Brand = _brandsManager.Read(Product.Brand.Id);
            Product.Category = _categoriesManager.Read(Product.Category.Id);

            return Product;
        }

        public void Add(Product Product)
        {
            SetBrandId(Product);
            SetCategoryId(Product);

            try
            {
                _dataAccess.SetQuery("insert into Products (Code, ProductName, ProductDescription, Price, BrandId, CategoryId) values (@Code, @ProductName, @ProductDescription, @Price, @BrandId, @CategoryId)");
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
            SetCategoryId(product);
            SetImages(product);

            try
            {
                _dataAccess.SetQuery("update Products set Code = @Code, ProductName = @ProductName, ProductDescription = @ProductDescription, Price = @Price, BrandId = @BrandId, CategoryId = @CategoryId where ProductId = @ProductId");
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
                _categoriesManager.PurgeCategory(product.Category);
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
            _dataAccess.SetParameter("@Precio", product.Price);
            _dataAccess.SetParameter("@BrandId", product.Brand?.Id);
            _dataAccess.SetParameter("@CategoryId", product.Category?.Id);
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

        private void SetCategoryId(Product product)
        {
            if (product.Category != null)
            {
                int dbCategoryId = _categoriesManager.GetId(product.Category);

                if (dbCategoryId == 0)
                {
                    _categoriesManager.Add(product.Category);
                    product.Category.Id = Helper.GetLastId("Categories");
                }
                else
                {
                    product.Category.Id = dbCategoryId;
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
                        image.Id = Helper.GetLastId("imagenes");
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
