using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class BrandsManager
    {
        private readonly DataAccess _dataAccess = new DataAccess();

        public List<Brand> List()
        {
            List<Brand> brands = new List<Brand>();

            try
            {
                _dataAccess.SetQuery("select BrandId, BrandDescription from Brands");
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Brand brand = new Brand();

                    brand.Id = (int)_dataAccess.Reader["BrandId"];
                    brand.Description = _dataAccess.Reader["BrandDescription"]?.ToString();
                    brand.Description = brand.Description ?? "";

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
                _dataAccess.SetQuery("select BrandDescription from Brands where BrandId = @BrandId");
                _dataAccess.SetParameter("@BrandId", brandId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    brand.Id = brandId;

                    brand.Description = _dataAccess.Reader["BrandDescription"]?.ToString();
                    brand.Description = brand.Description ?? "";
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
                _dataAccess.SetQuery("insert into Brands (BrandDescription) values (@BrandDescription)");
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
                _dataAccess.SetQuery("update Brands set BrandDescription = @BrandDescription where BrandId = @BrandId");
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

        public void Delete(Brand brand)
        {
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

        public int GetId(Brand brand)
        {
            if (brand == null)
            {
                return 0;
            }

            int id = 0;

            try
            {
                _dataAccess.SetQuery("select BrandId from Brands where BrandDescription = @BrandDescription");
                _dataAccess.SetParameter("@BrandDescription", brand.Description);
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

        /// <summary>
        /// Verifica si la marca del artículo no pertenece a ningun otro y en tal caso la elimina.
        /// </summary>
        public void PurgeBrand(Brand brand)
        {
            bool brandInUse = BrandIsInUse(brand);
            Debug.Print($"Verificando si la marca {brand} está en uso => {brandInUse}");

            if (!brandInUse)
            {
                Delete(brand);
            }
        }

        private bool BrandIsInUse(Brand brand)
        {
            try
            {
                _dataAccess.SetQuery(
                    "select count(*) as Total from Products where BrandId = @BrandId"
                );
                _dataAccess.SetParameter("@BrandId", brand.Id);
                return _dataAccess.ExecuteScalar() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Ocurrió un error al verificar si la marca {brand?.Description} existe.",
                    ex
                );
            }
            finally
            {
                _dataAccess.CloseConnection();
            }
        }

        private void SetParameters(Brand brand)
        {
            if (brand.Description != null)
            {
                _dataAccess.SetParameter("@BrandDescription", brand.Description);
            }
        }
    }
}
