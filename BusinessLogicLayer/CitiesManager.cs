using System;
using System.Collections.Generic;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class CitiesManager
    {
        // ATTRIBUTES

        private DataAccess _dataAccess = new DataAccess();

        // METHODS

        public List<City> List(int provinceId = 0)
        {
            List<City> citiesList = new List<City>();
            string query = "select CityId, CityName, ZipCode from Cities";

            if (0 < provinceId)
            {
                query += " where ProvinceId = @ProvinceId";
                _dataAccess.SetParameter("@ProvinceId", provinceId);
            }

            try
            {
                _dataAccess.SetQuery(query);
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    City city = new City();

                    city.Id = Convert.ToInt32(_dataAccess.Reader["CityId"]);
                    city.Name = (string)_dataAccess.Reader["CityName"];

                    if (!(_dataAccess.Reader["ZipCode"] is DBNull))
                    {
                        city.ZipCode = (string)_dataAccess.Reader["ZipCode"];
                    }

                    citiesList.Add(city);
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

            return citiesList;
        }

        public void Add(City city, int provinceId)
        {
            try
            {
                _dataAccess.SetQuery("insert into Cities (CityName, ZipCode, ProvinceId) values (@CityName, @ZipCode, @ProvinceId)");
                SetParameters(city, provinceId);
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

        public void Edit(City city, int provinceId)
        {
            try
            {
                _dataAccess.SetQuery("update Cities set CityName = @CityName, ZipCode = @ZipCode, ProvinceId = @ProvinceId where CityId = @CityId");
                _dataAccess.SetParameter("@CityId", city.Id);
                SetParameters(city, provinceId);
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

        public int GetId(City city)
        {
            if (city == null)
            {
                return 0;
            }

            int cityId = 0;

            try
            {
                _dataAccess.SetQuery("select CityId from Cities where CityName = @CityName");
                _dataAccess.SetParameter("@CityName", city.Name);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    cityId = Convert.ToInt32(_dataAccess.Reader["CityId"]);
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

            return cityId;
        }

        private void SetParameters(City city, int provinceId)
        {
            _dataAccess.SetParameter("@CityName", city.Name);
            _dataAccess.SetParameter("@ZipCode", city.ZipCode);
            _dataAccess.SetParameter("@ProvinceId", provinceId);
        }
    }
}
