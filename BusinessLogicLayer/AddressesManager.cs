using System;
using DataAccessLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace BusinessLogicLayer
{
    public class AddressesManager
    {
        // ATTRIBUTES

        private DataAccess _dataAccess = new DataAccess();
        private CountriesManager _countriesManager = new CountriesManager();
        private ProvincesManager _provincesManager = new ProvincesManager();
        private CitiesManager _citiesManager = new CitiesManager();

        // METHODS

        public Address Read(int addressId)
        {
            Address address = new Address();

            try
            {
                _dataAccess.SetQuery(
                    "select "
                        + "A.AddressId, A.StreetName, A.StreetNumber, A.Flat, A.Details, A.CityId, "
                        + "CI.CityName, CI.ZipCode, CI.ProvinceId, "
                        + "P.ProvinceName, P.CountryId, CO.CountryName "
                        + "from Addresses A "
                        + "inner join Cities CI on A.CityId = CI.CityId "
                        + "inner join Provinces P on CI.ProvinceId = P.ProvinceId "
                        + "inner join Countries CO on P.CountryId = CO.CountryId "
                        + "where AddressId = @AddressId"
                );
                _dataAccess.SetParameter("@AddressId", addressId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    address.Id = (int)_dataAccess.Reader["AddressId"];
                    address.StreetName = (string)_dataAccess.Reader["StreetName"];
                    address.StreetNumber = (string)_dataAccess.Reader["StreetNumber"];

                    if (!(_dataAccess.Reader["Flat"] is DBNull))
                    {
                        address.Flat = (string)_dataAccess.Reader["Flat"];
                    }

                    if (!(_dataAccess.Reader["Details"] is DBNull))
                    {
                        address.Details = (string)_dataAccess.Reader["Details"];
                    }

                    address.City.Id = Convert.ToInt32(_dataAccess.Reader["CityId"]);
                    address.City.Name = (string)_dataAccess.Reader["CityName"];

                    if (!(_dataAccess.Reader["ZipCode"] is DBNull))
                    {
                        address.City.ZipCode = (string)_dataAccess.Reader["ZipCode"];
                    }

                    address.Province.Id = Convert.ToInt32(_dataAccess.Reader["ProvinceId"]);
                    address.Province.Name = (string)_dataAccess.Reader["ProvinceName"];
                    address.Country.Id = Convert.ToInt32(_dataAccess.Reader["CountryId"]);
                    address.Country.Name = (string)_dataAccess.Reader["CountryName"];
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

            return address;
        }

        public void Add(Address address)
        {
            int dbCountryId = _countriesManager.GetId(address.Country);

            if (dbCountryId == 0)
            {
                _countriesManager.Add(address.Country);
                address.Country.Id = Helper.GetLastId("Countries");
            }
            else
            {
                address.Country.Id = dbCountryId;
            }

            int dbProvinceId = _provincesManager.GetId(address.Province);

            if (dbProvinceId == 0)
            {
                _provincesManager.Add(address.Province, address.Country.Id);
                address.Province.Id = Helper.GetLastId("Provinces");
            }
            else
            {
                address.Province.Id = dbProvinceId;
            }

            int dbCityId = _citiesManager.GetId(address.City);

            if (dbCityId == 0)
            {
                _citiesManager.Add(address.City, address.Province.Id);
                address.City.Id = Helper.GetLastId("Cities");
            }
            else
            {
                address.City.Id = dbCityId;
            }

            try
            {
                _dataAccess.SetQuery(
                    "insert into Addresses (StreetName, StreetNumber, Flat, Details, CityId) values (@StreetName, @StreetNumber, @Flat, @Details, @CityId)"
                );
                SetParameters(address);
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

        public void Edit(Address address)
        {
            int dbCountryId = _countriesManager.GetId(address.Country);

            if (dbCountryId == 0)
            {
                _countriesManager.Add(address.Country);
                address.Country.Id = Helper.GetLastId("Countries");
            }
            else if (dbCountryId == address.Country.Id)
            {
                _countriesManager.Edit(address.Country);
            }
            else
            {
                address.Country.Id = dbCountryId;
            }

            int dbProvinceId = _provincesManager.GetId(address.Province);

            if (dbProvinceId == 0)
            {
                _provincesManager.Add(address.Province, address.Country.Id);
                address.Province.Id = Helper.GetLastId("Provinces");
            }
            else if (dbProvinceId == address.Province.Id)
            {
                _provincesManager.Edit(address.Province, address.Country.Id);
            }
            else
            {
                address.Province.Id = dbProvinceId;
            }

            int dbCityId = _citiesManager.GetId(address.City);

            if (dbCityId == 0)
            {
                _citiesManager.Add(address.City, address.Province.Id);
                address.City.Id = Helper.GetLastId("Cities");
            }
            else if (dbCityId == address.City.Id)
            {
                _citiesManager.Edit(address.City, address.Province.Id);
            }
            else
            {
                address.City.Id = dbCityId;
            }

            try
            {
                _dataAccess.SetQuery(
                    "update Addresses set StreetName = @StreetName, StreetNumber = @StreetNumber, Flat = @Flat, Details = @Details, CityId = @CityId where AddressId = @AddressId"
                );
                _dataAccess.SetParameter("@AddressId", address.Id);
                SetParameters(address);
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

        public int GetId(Address address)
        {
            if (address == null || address.IsEmpty())
            {
                return 0;
            }

            int addressId = 0;

            try
            {
                _dataAccess.SetQuery(
                    "select  Top 1  AddressId  from Addresses where StreetName = @StreetName and StreetNumber = @StreetNumber and CityId = @CityId"
                );
                _dataAccess.SetParameter("@StreetName", address.StreetName);
                _dataAccess.SetParameter("@StreetNumber", address.StreetNumber);
                _dataAccess.SetParameter("@CityId", address.City.Id);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    addressId = (int)_dataAccess.Reader["AddressId"];
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

            return addressId;
        }

        public void HandleAddressId(Person person)
        {
            if (person.Address != null && !person.Address.IsEmpty())
            {
                int foundAddressId = GetId(person.Address);

                if (foundAddressId == 0)
                {
                    Add(person.Address);
                    person.Address.Id = Helper.GetLastId("Addresses");
                }
                else if (foundAddressId == person.Address.Id)
                {
                    Edit(person.Address);
                }
                else
                {
                    person.Address.Id = foundAddressId;
                }
            }
        }

        public void HandleDeliveryAddressId(Order order) // hack : unificar con HandleAddressId(Person person) y eventualmente crear funcion generica
        {
            if (order.DeliveryAddress != null)
            {
                int foundDeliveryAddressId = GetId(order.DeliveryAddress);

                if (foundDeliveryAddressId == 0)
                {
                    Add(order.DeliveryAddress);
                    order.DeliveryAddress.Id = Helper.GetLastId("Addresses");
                }
                else if (foundDeliveryAddressId == order.DeliveryAddress.Id)
                {
                    Edit(order.DeliveryAddress);
                }
                else
                {
                    order.DeliveryAddress.Id = foundDeliveryAddressId;
                }
            }
        }

        private void SetParameters(Address address)
        {
            _dataAccess.SetParameter("@StreetName", address.StreetName);
            _dataAccess.SetParameter("@StreetNumber", address.StreetNumber);
            _dataAccess.SetParameter("@Flat", address.Flat);
            _dataAccess.SetParameter("@Details", address.Details);
            _dataAccess.SetParameter("@CityId", address.City.Id);
        }
    }
}
