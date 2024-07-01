using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class BrandsManager
    {
        private DataAccess _dataAccess = new DataAccess();

        public List<Brand> List(bool onlyActive = true)
        {
            List<Brand> brands = new List<Brand>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Brands");
                _dataAccess.SetParameter("@OnlyActive", onlyActive);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Brand brand = new Brand();

                    brand.Id = (int)_dataAccess.Reader["BrandId"];
                    brand.IsActive = (bool)_dataAccess.Reader["IsActive"];
                    brand.Name = _dataAccess.Reader["BrandName"]?.ToString();
                    brand.Name = brand.Name ?? "";

                    brands.Add(brand);
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

            return brands;
        }

        public Brand Read(int brandId)
        {
            Brand brand = new Brand();

            try
            {
                _dataAccess.SetQuery("select IsActive, BrandName from Brands where BrandId = @BrandId");
                _dataAccess.SetParameter("@BrandId", brandId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    brand.Id = brandId;
                    brand.IsActive = (bool)_dataAccess.Reader["IsActive"];
                    brand.Name = _dataAccess.Reader["BrandName"]?.ToString();
                    brand.Name = brand.Name ?? "";
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

            return brand;
        }

        public void Add(Brand brand)
        {
            try
            {
                _dataAccess.SetQuery("insert into Brands (BrandName) values (@BrandName)");
                SetParameters(brand);
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

        public void Edit(Brand brand)
        {
            try
            {
                _dataAccess.SetQuery(
                    "update Brands set BrandName = @BrandName where BrandId = @BrandId"
                );
                _dataAccess.SetParameter("@BrandId", brand.Id);
                SetParameters(brand);
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

        public void DeleteLogically(Brand brand)
        {
            try
            {
                _dataAccess.SetProcedure("SP_Delete_Brand_Logically");
                _dataAccess.SetParameter("@BrandId", brand.Id);
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

        public void Delete(Brand brand, bool isLogicalDeletion = true)
        {
            if (isLogicalDeletion == true)
            {
                DeleteLogically(brand);
                return;
            }

            try
            {
                _dataAccess.SetQuery("delete from Brands where BrandId = @BrandId");
                _dataAccess.SetParameter("@BrandId", brand.Id);
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

        /// <summary>
        /// Verifica si la marca del artículo no pertenece a ningun otro y en tal caso la elimina.
        /// </summary>
        public void PurgeBrand(Brand brand)
        {
            if (CountBrandRelations(brand) == 0)
            {
                Delete(brand);
            }
        }

        public int CountBrandRelations(Brand brand)
        {
            try
            {
                _dataAccess.SetProcedure("SP_Count_Brand_Relations");
                _dataAccess.SetParameter("@BrandId", brand.Id);
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

        public int GetId(Brand brand)
        {
            if (brand == null)
            {
                return 0;
            }

            int id = 0;

            try
            {
                _dataAccess.SetQuery("select BrandId from Brands where BrandName = @BrandName");
                _dataAccess.SetParameter("@BrandName", brand.Name);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    id = (int)_dataAccess.Reader["BrandId"];
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

        private void SetParameters(Brand brand)
        {
            _dataAccess.SetParameter("@IsActive", brand.IsActive);

            if (brand.Name != null)
            {
                _dataAccess.SetParameter("@BrandName", brand.Name);
            }
        }

        private List<int> GetBrandsIds(int productId)
        {
            List<int> brandsId = new List<int>();

            return brandsId;
        }

        public bool AlreadyExists(Brand brand)
        {
            if (brand == null)
            {
                return false;
            }

            int id = 0;

            try
            {
                _dataAccess.SetQuery("select BrandId from Brands where BrandId = @BrandId");
                _dataAccess.SetParameter("@BrandId", brand.Id);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    id = (int)_dataAccess.Reader["BrandId"];
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

            return id != 0;
        }
    }
}
