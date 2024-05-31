using System;
using System.Collections.Generic;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class ProvincesManager
    {
        // ATTRIBUTES

        private DataAccess _dataAccess = new DataAccess();

        // METHODS

        public List<Province> List(int countryId = 0)
        {
            List<Province> provincesList = new List<Province>();
            string query = "select ProvinceId, ProvinceName from Provinces";

            if (0 < countryId)
            {
                query += " where CountryId = @CountryId";
                _dataAccess.SetParameter("@CountryId", countryId);
            }

            try
            {
                _dataAccess.SetQuery(query);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Province province = new Province();

                    province.Id = Convert.ToInt32(_dataAccess.Reader["ProvinceId"]);
                    province.Name = (string)_dataAccess.Reader["ProvinceName"];

                    provincesList.Add(province);
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

            return provincesList;
        }

        public void Add(Province province, int countryId)
        {
            try
            {
                _dataAccess.SetQuery("insert into Provinces (ProvinceName, CountryId) values (@ProvinceName, @CountryId)");
                SetParameters(province, countryId);
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

        public void Edit(Province province, int countryId)
        {
            try
            {
                _dataAccess.SetQuery("update Provinces set ProvinceName = @ProvinceName, CountryId = @CountryId where ProvinceId = @ProvinceId");
                _dataAccess.SetParameter("@ProvinceId", province.Id);
                SetParameters(province, countryId);
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

        public int GetId(Province province)
        {
            if (province == null)
            {
                return 0;
            }

            int provinceId = 0;

            try
            {
                _dataAccess.SetQuery("select ProvinceId from Provinces where ProvinceName = @ProvinceName");
                _dataAccess.SetParameter("@ProvinceName", province.Name);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    provinceId = Convert.ToInt32(_dataAccess.Reader["ProvinceId"]);
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

            return provinceId;
        }

        private void SetParameters(Province province, int countryId)
        {
            _dataAccess.SetParameter("@ProvinceName", province.Name);
            _dataAccess.SetParameter("@CountryId", countryId);
        }
    }
}
