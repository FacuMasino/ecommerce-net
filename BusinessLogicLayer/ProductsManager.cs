﻿using System;
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

        public List<ProductType> List<ProductType>(
            bool onlyActive = true,
            bool onlyAvailable = false,
            int orderId = 0
        )
            where ProductType : Product, new()
        {
            List<ProductType> productTypeList = new List<ProductType>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Products");
                _dataAccess.SetParameter("@OnlyActive", onlyActive);
                _dataAccess.SetParameter("@OrderId", orderId);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    ProductType productType = new ProductType();

                    productType.Id = (int)_dataAccess.Reader["ProductId"];
                    productType.IsActive = (bool)_dataAccess.Reader["IsActive"];
                    productType.Code = _dataAccess.Reader["Code"]?.ToString();
                    productType.Name = _dataAccess.Reader["ProductName"]?.ToString();
                    productType.Description = _dataAccess.Reader["ProductDescription"]?.ToString();
                    productType.Price =
                        _dataAccess.Reader["Price"] as decimal? ?? productType.Price;
                    productType.Cost = _dataAccess.Reader["Cost"] as decimal? ?? productType.Cost;
                    productType.Stock =
                        (int)_dataAccess.Reader["Stock"] as int? ?? productType.Stock;
                    productType.Brand.Id =
                        _dataAccess.Reader["BrandId"] as int? ?? productType.Brand.Id;
                    productType.Categories = _categoriesManager.List(true, productType.Id);
                    productType.Images = _imagesManager.List(productType.Id);

                    if (productType is ProductSet)
                    {
                        (productType as ProductSet).Quantity =
                            (int)_dataAccess.Reader["Quantity"] as int?
                            ?? (productType as ProductSet).Quantity;
                    }

                    if (onlyAvailable & productType.Stock == 0)
                    {
                        continue; // hack : No agregar si no tiene stock
                    }

                    productTypeList.Add(productType);
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

            foreach (ProductType productType in productTypeList)
            {
                productType.Brand = _brandsManager.Read(productType.Brand.Id);
            }

            return productTypeList;
        }

        public Product Read(int productId, bool onlyActive = true)
        {
            Product product = new Product();

            try
            {
                string queryCondition = "ProductId = @ProductId And IsActive = 1";
                if (!onlyActive)
                    queryCondition = "ProductId = @ProductId";

                _dataAccess.SetQuery(
                    $"select Code, ProductName, ProductDescription, Price, Cost, Stock, BrandId, IsActive from Products where {queryCondition}"
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
                    product.Categories = _categoriesManager.List(true, product.Id);
                    product.Images = _imagesManager.List(product.Id);
                    product.IsActive = (bool)_dataAccess.Reader["IsActive"];
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

        public void Add(Product product)
        {
            SetBrandId(product);

            try
            {
                _dataAccess.SetQuery(
                    "insert into Products (Code, ProductName, ProductDescription, Price, Cost, Stock, BrandId, IsActive) values (@Code, @ProductName, @ProductDescription, @Price, @Cost, @Stock, @BrandId, @IsActive)"
                );
                SetParameters(product);
                _dataAccess.ExecuteAction();
                SetImages(product); // Las imagenes se agregan luego de agregar el articulo ya que van con le id del mismo asociadas
                SetCategories(product);
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
            _imagesManager.CheckDeleted(product); // Despues de editar y agregar las nuevas, verificar/eliminar el resto
            _categoriesManager.UpdateRelations(product); // Se revisan y actualizan las relaciones de categorias

            try
            {
                _dataAccess.SetQuery(
                    "update Products set Code = @Code, ProductName = @ProductName, ProductDescription = @ProductDescription, Price = @Price, Cost = @Cost, BrandId = @BrandId, Stock = @Stock, IsActive = @IsActive where ProductId = @ProductId"
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

        public void Delete(Product product)
        {
            try
            {
                _dataAccess.SetProcedure("SP_Delete_Product_Logically");
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

            if (product.Description != null)
            {
                _dataAccess.SetParameter("@ProductDescription", product.Description);
            }
            else
            {
                _dataAccess.SetParameter("@ProductDescription", DBNull.Value);
            }
            
            _dataAccess.SetParameter("@Price", product.Price);
            _dataAccess.SetParameter("@Cost", product.Cost);

            if (product.Brand != null)
            {
                _dataAccess.SetParameter("@BrandId", product.Brand.Id);
            }
            else
            {
                _dataAccess.SetParameter("@BrandId", DBNull.Value);
            }

            _dataAccess.SetParameter("@Stock", product.Stock);
            _dataAccess.SetParameter("@IsActive", product.IsActive);
        }

        private void SetBrandId(Product product)
        {
            if (product.Brand != null)
            {
                int dbBrandId = _brandsManager.AlreadyExists(product.Brand) ? product.Brand.Id : 0;

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
            int productId = product.Id == 0 ? Helper.GetLastId("Products") : product.Id; // si es un producto nuevo, se obtiene el id nuevo

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

        /// <summary>
        /// Agrega las relaciones Producto-Categoria en la tabla correspondiente
        /// </summary>
        private void SetCategories(Product product)
        {
            int productId = product.Id == 0 ? Helper.GetLastId("Products") : product.Id; // si es un articulo nuevo, se obtiene el id nuevo

            foreach (Category category in product.Categories)
            {
                if (category != null)
                {
                    _categoriesManager.AddRelation(category, productId);
                }
            }
        }

        /// <summary>
        /// Comprueba si existe un producto que ya esté usando el código proporcionado
        /// </summary>
        /// <param name="code">Código de producto</param>
        public bool ProductCodeExists(string code)
        {
            try
            {
                _dataAccess.SetQuery("select ProductId from Products where Code = @Code");
                _dataAccess.SetParameter("@Code", code);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    if (Convert.IsDBNull(_dataAccess.Reader["ProductId"]))
                        return false;
                    return true;
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
            return false;
        }

        public void AddVisit(int productId)
        {
            try
            {
                _dataAccess.SetProcedure("SP_Add_Visit");
                _dataAccess.SetParameter("@ProductId", productId);
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
    }
}
