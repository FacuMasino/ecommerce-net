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
            List<Product> Products = new List<Product>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Products");
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Product Product = new Product();

                    Product.Id = (int)_dataAccess.Reader["Id"];

                    Product.Code = _dataAccess.Reader["Codigo"]?.ToString();
                    Product.Name = _dataAccess.Reader["Nombre"]?.ToString();
                    Product.Description = _dataAccess.Reader["Descripcion"]?.ToString();

                    /*Acá intenta convertir IdMarca a un int "Nullable" si es null, entonces el operador ?.
                     * va a evaluar la expresión de la derecha y se queda con Product.Brand.Id
                     * , sino se asigna el IdMarca obtenido */
                    Product.Brand.Id = _dataAccess.Reader["IdMarca"] as int? ?? Product.Brand.Id;
                    Product.Category.Id =
                        _dataAccess.Reader["IdCategoria"] as int? ?? Product.Category.Id;
                    Product.Price = _dataAccess.Reader["Precio"] as decimal? ?? Product.Price;

                    Product.Images = _imagesManager.List(Product.Id);

                    Products.Add(Product);
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

            foreach (Product Product in Products)
            {
                Product.Brand = _brandsManager.Read(Product.Brand.Id);
                Product.Category = _categoriesManager.Read(Product.Category.Id);
            }

            return Products;
        }

        public Product Read(int id)
        {
            Product Product = new Product();

            try
            {
                _dataAccess.SetQuery(
                    "select Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio from Articulos where Id = @Id"
                );
                _dataAccess.SetParameter("@Id", id);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    Product.Id = id;

                    Product.Code = _dataAccess.Reader["Codigo"]?.ToString();
                    Product.Name = _dataAccess.Reader["Nombre"]?.ToString();
                    Product.Description = _dataAccess.Reader["Descripcion"]?.ToString();
                    Product.Brand.Id = _dataAccess.Reader["IdMarca"] as int? ?? Product.Brand.Id;
                    Product.Category.Id =
                        _dataAccess.Reader["IdCategoria"] as int? ?? Product.Category.Id;

                    if (!(_dataAccess.Reader["Precio"] is DBNull))
                    {
                        Product.Price = (decimal)_dataAccess.Reader["Precio"];
                    }

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
                _dataAccess.SetQuery(
                    "insert into Articulos (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)"
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

        public void Edit(Product Product)
        {
            SetBrandId(Product);
            SetCategoryId(Product);
            SetImages(Product);

            try
            {
                _dataAccess.SetQuery(
                    "update Articulos set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio where Id = @Id"
                );
                _dataAccess.SetParameter("@Id", Product.Id);
                SetParameters(Product);
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

        public void Delete(Product Product, bool purgeBrand = false, bool purgeCategory = false)
        {
            try
            {
                _dataAccess.SetQuery("delete from Articulos where Id = @Id");
                _dataAccess.SetParameter("@Id", Product.Id);
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
                _brandsManager.PurgeBrand(Product.Brand);
            }

            if (purgeCategory)
            {
                _categoriesManager.PurgeCategory(Product.Category);
            }
        }

        public int GetId(Product Product)
        {
            if (Product == null)
            {
                return 0;
            }

            int id = 0;

            try
            {
                _dataAccess.SetQuery("select Id from Articulos where Codigo = @Codigo");
                _dataAccess.SetParameter("@Codigo", Product.Code);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    id = (int)_dataAccess.Reader["Id"];
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

        private void SetParameters(Product Product)
        {
            SetParameterIfNotNull("@Codigo", Product.Code);

            SetParameterIfNotNull("@Nombre", Product.Name);
            SetParameterIfNotNull("@Descripcion", Product.Description);
            SetParameterIfNotNull("@IdMarca", Product.Brand?.Id);
            SetParameterIfNotNull("@IdCategoria", Product.Category?.Id);

            _dataAccess.SetParameter("@Precio", Product.Price);
        }

        private void SetParameterIfNotNull(string parameterKey, object value)
        {
            if (value != null)
            {
                _dataAccess.SetParameter(parameterKey, value);
            }
        }

        private void SetBrandId(Product Product)
        {
            if (Product.Brand != null)
            {
                int dbBrandId = _brandsManager.GetId(Product.Brand);

                if (dbBrandId == 0)
                {
                    _brandsManager.Add(Product.Brand);
                    Product.Brand.Id = Helper.GetLastId("Marcas");
                }
                else
                {
                    Product.Brand.Id = dbBrandId;
                }
            }
        }

        private void SetCategoryId(Product Product)
        {
            if (Product.Category != null)
            {
                int dbCategoryId = _categoriesManager.GetId(Product.Category);

                if (dbCategoryId == 0)
                {
                    _categoriesManager.Add(Product.Category);
                    Product.Category.Id = Helper.GetLastId("Categorias");
                }
                else
                {
                    Product.Category.Id = dbCategoryId;
                }
            }
        }

        private void SetImages(Product Product)
        {
            int ProductId = Product.Id == 0 ? Helper.GetLastId("Articulos") : Product.Id; // si es un articulo nuevo, se obtiene el id nuevo

            foreach (var image in Product.Images)
            {
                if (image != null)
                {
                    if (image.Id == 0) // si es una imagen nueva, se agrega y obtiene id
                    {
                        _imagesManager.Add(image, ProductId);
                        image.Id = Helper.GetLastId("imagenes");
                        Debug.Print("imagen: " + image.Id.ToString());
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
