using System;
using System.Collections.Generic;
using System.Web.Security;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class CountriesManager
    {
        private DataAccess _dataAccess = new DataAccess();

        public enum Ids
        {
            ArgentinaId = 1
        }

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

                    country.Id = Convert.ToInt32(_dataAccess.Reader["CountryId"]);
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

        public Country Read(int countryId)
        {
            Country country = new Country();

            try
            {
                _dataAccess.SetQuery("select CountryName from Countries where CountryId = @CountryId");
                _dataAccess.SetParameter("@CountryId", countryId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    country.Id = countryId;
                    country.Name = _dataAccess.Reader["CountryName"].ToString();
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

            return country;
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
                _dataAccess.SetParameter("@CountryId", country.Id);
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
