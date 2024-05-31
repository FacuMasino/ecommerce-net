using System;
using DataAccessLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace BusinessLogicLayer
{
    public class AdressesManager
    {
        // ATTRIBUTES

        private DataAccess _dataAccess = new DataAccess();
        private CountriesManager _countriesManager = new CountriesManager();
        private ProvincesManager _provincesManager = new ProvincesManager();
        private CitiesManager _citiesManager = new CitiesManager();

        // METHODS

        public Adress Read(int adressId)
        {
            Adress adress = new Adress();

            try
            {
                _dataAccess.SetQuery(
                    "select " +
                    "A.AdressId, A.StreetName, A.StreetNumber, A.Flat, A.Details, A.CityId, " +
                    "CI.CityName, CI.ZipCode, CI.ProvinceId, " +
                    "P.ProvinceName, P.CountryId, CO.CountryName " +
                    "from Adresses A " +
                    "inner join Cities CI on A.CityId = CI.CityId " +
                    "inner join Provinces P on CI.ProvinceId = P.ProvinceId " +
                    "inner join Countries CO on P.CountryId = CO.CountryId " +
                    "where AdressId = @AdressId"
                );
                _dataAccess.SetParameter("@AdressId", adressId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    adress.Id = (int)_dataAccess.Reader["AdressId"];
                    adress.StreetName = (string)_dataAccess.Reader["StreetName"];
                    adress.StreetNumber = (string)_dataAccess.Reader["StreetNumber"];

                    if (!(_dataAccess.Reader["Flat"] is DBNull))
                    {
                        adress.Flat = (string)_dataAccess.Reader["Flat"];
                    }

                    if (!(_dataAccess.Reader["Details"] is DBNull))
                    {
                        adress.Details = (string)_dataAccess.Reader["Details"];
                    }

                    adress.City.Id = Convert.ToInt32(_dataAccess.Reader["CityId"]);
                    adress.City.Name = (string)_dataAccess.Reader["CityName"];

                    if (!(_dataAccess.Reader["ZipCode"] is DBNull))
                    {
                        adress.City.ZipCode = (string)_dataAccess.Reader["ZipCode"];
                    }

                    adress.Province.Id = Convert.ToInt32(_dataAccess.Reader["ProvinceId"]);
                    adress.Province.Name = (string)_dataAccess.Reader["ProvinceName"];
                    adress.Country.Id = Convert.ToInt32(_dataAccess.Reader["CountryId"]);
                    adress.Country.Name = (string)_dataAccess.Reader["CountryName"];
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

            return adress;
        }

        public void Add(Adress adress)
        {
            int dbCountryId = _countriesManager.GetId(adress.Country);
            
            if (dbCountryId == 0)
            {
                _countriesManager.Add(adress.Country);
                adress.Country.Id = Helper.GetLastId("Countries");
            }
            else
            {
                adress.Country.Id = dbCountryId;
            }

            int dbProvinceId = _provincesManager.GetId(adress.Province);
            
            if (dbProvinceId == 0)
            {
                _provincesManager.Add(adress.Province, adress.Country.Id);
                adress.Province.Id = Helper.GetLastId("Provinces");
            }
            else
            {
                adress.Province.Id = dbProvinceId;
            }

            int dbCityId = _citiesManager.GetId(adress.City);
            
            if (dbCityId == 0)
            {
                _citiesManager.Add(adress.City, adress.Province.Id);
                adress.City.Id = Helper.GetLastId("Cities");
            }
            else
            {
                adress.City.Id = dbCityId;
            }

            try
            {
                _dataAccess.SetQuery("insert into Adresses (StreetName, StreetNumber, Flat, Details, CityId) values (@StreetName, @StreetNumber, @Flat, @Details, @CityId)");
                SetParameters(adress);
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

        public void Edit(Adress adress)
        {
            int dbCountryId = _countriesManager.GetId(adress.Country);

            if (dbCountryId == 0)
            {
                _countriesManager.Add(adress.Country);
                adress.Country.Id = Helper.GetLastId("Countries");
            }
            else if (dbCountryId == adress.Country.Id)
            {
                _countriesManager.Edit(adress.Country);
            }
            else
            {
                adress.Country.Id = dbCountryId;
            }

            int dbProvinceId = _provincesManager.GetId(adress.Province);

            if (dbProvinceId == 0)
            {
                _provincesManager.Add(adress.Province, adress.Country.Id);
                adress.Province.Id = Helper.GetLastId("Provinces");
            }
            else if (dbProvinceId == adress.Province.Id)
            {
                _provincesManager.Edit(adress.Province, adress.Country.Id);
            }
            else
            {
                adress.Province.Id = dbProvinceId;
            }

            int dbCityId = _citiesManager.GetId(adress.City);

            if (dbCityId == 0)
            {
                _citiesManager.Add(adress.City, adress.Province.Id);
                adress.City.Id = Helper.GetLastId("Cities");
            }
            else if (dbCityId == adress.City.Id)
            {
                _citiesManager.Edit(adress.City, adress.Province.Id);
            }
            else
            {
                adress.City.Id = dbCityId;
            }

            try
            {
                _dataAccess.SetQuery("update Adresses set StreetName = @StreetName, StreetNumber = @StreetNumber, Flat = @Flat, Details = @Details, CityId = @CityId where AdressId = @AdressId");
                _dataAccess.SetParameter("@AdressId", adress.Id);
                SetParameters(adress);
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

        public int GetId(Adress adress)
        {
            if (adress == null)
            {
                return 0;
            }

            int adressId = 0;

            try
            {
                _dataAccess.SetQuery("select AdressId from Adresses where StreetName = @StreetName and StreetNumber = @StreetNumber and CityId = @CityId");
                _dataAccess.SetParameter("@StreetName", adress.StreetName);
                _dataAccess.SetParameter("@StreetNumber", adress.StreetNumber);
                _dataAccess.SetParameter("@CityId", adress.City.Id);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    adressId = (int)_dataAccess.Reader["AdressId"];
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

            return adressId;
        }

        private void SetParameters(Adress adress)
        {
            _dataAccess.SetParameter("@StreetName", adress.StreetName);
            _dataAccess.SetParameter("@StreetNumber", adress.StreetNumber);
            _dataAccess.SetParameter("@Flat", adress.Flat);
            _dataAccess.SetParameter("@Details", adress.Details);
            _dataAccess.SetParameter("@CityId", adress.City.Id);
        }
    }
}
