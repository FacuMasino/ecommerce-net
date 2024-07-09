using System;
using System.Collections.Generic;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class StatsManager
    {
        private DataAccess _dataAccess = new DataAccess();
        private CategoriesManager _categoriesManager = new CategoriesManager();
        private BrandsManager _brandsManager = new BrandsManager();
        private ImagesManager _imagesManager = new ImagesManager();

        public int CountFinishedOrders()
        {
            try
            {
                _dataAccess.SetProcedure("SP_Count_Finished_Orders");
                return _dataAccess.ExecuteScalar();
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

        public int CountActiveProducts()
        {
            try
            {
                _dataAccess.SetProcedure("SP_Count_Active_Products");
                return _dataAccess.ExecuteScalar();
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

        public int CountSoldProducts()
        {
            try
            {
                _dataAccess.SetProcedure("SP_Count_Sold_Products");
                return _dataAccess.ExecuteScalar();
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

        public int CountShippedProducts()
        {
            try
            {
                _dataAccess.SetProcedure("SP_Count_Shipped_Products");
                return _dataAccess.ExecuteScalar();
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

        public List<ProductStats> TopSellingProducts()
        {
            List<ProductStats> topSellingList = new List<ProductStats>();

            try
            {
                _dataAccess.SetQuery("Select Top 10 * From dbo.VW_TopSellingProducts");
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    ProductStats productStats = new ProductStats();

                    productStats.Id = (int)_dataAccess.Reader["ProductId"];
                    productStats.IsActive = (bool)_dataAccess.Reader["IsActive"];
                    productStats.Code = _dataAccess.Reader["Code"]?.ToString();
                    productStats.Name = _dataAccess.Reader["ProductName"]?.ToString();
                    productStats.Price =
                        _dataAccess.Reader["Price"] as decimal? ?? productStats.Price;
                    productStats.Brand.Id =
                        _dataAccess.Reader["BrandId"] as int? ?? productStats.Brand.Id;
                    productStats.Categories = _categoriesManager.List(true, productStats.Id);
                    productStats.Images = _imagesManager.List(productStats.Id);
                    productStats.TotalQuantitySold =
                        _dataAccess.Reader["TotalQuantitySold"] as int?
                        ?? productStats.TotalQuantitySold;

                    topSellingList.Add(productStats);
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

            foreach (ProductStats product in topSellingList)
            {
                product.Brand = _brandsManager.Read(product.Brand.Id);
            }

            return topSellingList;
        }

        public List<ProductStats> TopVisitedProducts()
        {
            List<ProductStats> topSellingList = new List<ProductStats>();

            try
            {
                _dataAccess.SetQuery("Select Top 10 * From dbo.VW_TopVisitedProducts");
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    ProductStats productStats = new ProductStats();

                    productStats.Id = (int)_dataAccess.Reader["ProductId"];
                    productStats.IsActive = (bool)_dataAccess.Reader["IsActive"];
                    productStats.Code = _dataAccess.Reader["Code"]?.ToString();
                    productStats.Name = _dataAccess.Reader["ProductName"]?.ToString();
                    productStats.Description = _dataAccess.Reader["ProductDescription"]?.ToString();
                    productStats.Price =
                        _dataAccess.Reader["Price"] as decimal? ?? productStats.Price;
                    productStats.Cost = _dataAccess.Reader["Cost"] as decimal? ?? productStats.Cost;
                    productStats.Stock =
                        (int)_dataAccess.Reader["Stock"] as int? ?? productStats.Stock;
                    productStats.Brand.Id =
                        _dataAccess.Reader["BrandId"] as int? ?? productStats.Brand.Id;
                    productStats.Categories = _categoriesManager.List(true, productStats.Id);
                    productStats.Images = _imagesManager.List(productStats.Id);
                    productStats.TotalVisits =
                        _dataAccess.Reader["TotalVisits"] as int? ?? productStats.TotalVisits;

                    topSellingList.Add(productStats);
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

            foreach (ProductStats product in topSellingList)
            {
                product.Brand = _brandsManager.Read(product.Brand.Id);
            }

            return topSellingList;
        }
    }
}
