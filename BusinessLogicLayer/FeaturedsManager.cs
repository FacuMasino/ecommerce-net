using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class FeaturedsManager
    {
        private DataAccess _dataAccess = new DataAccess();
        private ProductsManager _productsManager = new ProductsManager();

        public List<FeaturedProduct> List()
        {
            List<FeaturedProduct> featuredProducts = new List<FeaturedProduct>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Featured_Products");
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    int productId = (int)_dataAccess.Reader["ProductId"];

                    Product product = new Product();
                    product = _productsManager.Read(productId);

                    FeaturedProduct featuredProduct = new FeaturedProduct(product)
                    {
                        DisplayOrder = (int)_dataAccess.Reader["DisplayOrder"],
                        ShowAsNew = (bool)_dataAccess.Reader["ShowAsNew"]
                    };

                    featuredProducts.Add(featuredProduct);
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

            featuredProducts = featuredProducts.OrderBy(f => f.DisplayOrder).ToList();

            return featuredProducts;
        }

        public void Add(FeaturedProduct featuredProduct)
        {
            try
            {
                _dataAccess.SetQuery(
                    "insert into FeaturedProducts (ProductId, DisplayOrder, ShowAsNew)"
                        + "values (@ProductId, @DisplayOrder, @ShowAsNew)"
                );
                _dataAccess.SetParameter("@ProductId", featuredProduct.Id);
                _dataAccess.SetParameter("@DisplayOrder", featuredProduct.DisplayOrder);
                _dataAccess.SetParameter("@ShowAsNew", featuredProduct.ShowAsNew);
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

        // Es eliminacion fisica
        public void Delete(int productId)
        {
            try
            {
                _dataAccess.SetQuery("Delete from FeaturedProducts Where ProductId = @ProductId");
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
