using System;
using System.Collections.Generic;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class CountriesManager
    {
        // ATTRIBUTES

        private DataAccess _dataAccess = new DataAccess();

        // METHODS

        public List<Country> List()
        {
            List<Country> countriesList = new List<Country>();

            try
            {
                _dataAccess.SetQuery("select CountryId, CountryName from Countries");
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    Country country = new Country();

                    country.CountryId = Convert.ToInt32(_dataAccess.Reader["CountryId"]);
                    country.Name = (string)_dataAccess.Reader["CountryName"];

                    countriesList.Add(country);
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

            return countriesList;
        }

        public void Add(Country country)
        {
            try
            {
                _dataAccess.SetQuery("insert into Countries (CountryName) values (@CountryName)");
                SetParameters(country);
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

        public void Edit(Country country)
        {
            try
            {
                _dataAccess.SetQuery("update Countries set CountryName = @CountryName where CountryId = @CountryId");
                _dataAccess.SetParameter("@CountryId", country.CountryId);
                SetParameters(country);
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

        public int GetId(Country country)
        {
            if (country == null)
            {
                return 0;
            }

            int countryId = 0;

            try
            {
                _dataAccess.SetQuery("select CountryId from Countries where CountryName = @CountryName");
                _dataAccess.SetParameter("@CountryName", country.Name);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    countryId = Convert.ToInt32(_dataAccess.Reader["CountryId"]);
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

            return countryId;
        }

        private void SetParameters(Country country)
        {
            _dataAccess.SetParameter("@CountryName", country.Name);
        }
    }
}
