using System;
using System.Web.UI;
using DataAccessLayer;
using DomainModelLayer;
using UtilitiesLayer;

namespace BusinessLogicLayer
{
    public class UsersManager
    {
        private DataAccess _dataAccess = new DataAccess();
        private Person _person;
        private PeopleManager _peopleManager = new PeopleManager();
        private RolesManager _rolesManager = new RolesManager();

        public User Read(int userId)
        {
            User user = new User();

            try
            {
                _dataAccess.SetProcedure("SP_Read_User");
                _dataAccess.SetParameter("@UserId", userId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    user.UserId = userId;
                    user.Username = _dataAccess.Reader["Username"]?.ToString();
                    user.Username = user.Username ?? "";
                    user.Password = (string)_dataAccess.Reader["UserPassword"];
                    user.Role.Id = Convert.ToInt32(_dataAccess.Reader["RoleId"]);
                    user.PersonId = (int)_dataAccess.Reader["PersonId"];
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

            user.Role = _rolesManager.Read(user.Role.Id);

            _person = _peopleManager.Read(user.PersonId);
            Helper.AssignPerson(user, _person);

            return user;
        }

        public int Add(User user)
        {
            user.PersonId = _peopleManager.Add(user);

            if (user.Role != null)
            {
                int foundRoleId = _rolesManager.GetId(user.Role);

                if (foundRoleId == 0)
                {
                    user.Role.Id = _rolesManager.Add(user.Role);
                }
                else
                {
                    user.Role.Id = foundRoleId;
                }
            }

            int UserId = 0;

            try
            {
                _dataAccess.SetProcedure("SP_Add_User");
                SetParameters(user);
                UserId = _dataAccess.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            return UserId;
        }

        public void Edit(User user)
        {
            _peopleManager.Edit(user);

            if (user.Role != null)
            {
                int foundRoleId = _rolesManager.GetId(user.Role);

                if (foundRoleId == 0)
                {
                    user.Role.Id = _rolesManager.Add(user.Role);
                }
                else if (foundRoleId == user.Role.Id)
                {
                    _rolesManager.Edit(user.Role);
                }
                else
                {
                    user.Role.Id = foundRoleId;
                }
            }

            try
            {
                _dataAccess.SetProcedure("SP_Edit_User");
                _dataAccess.SetParameter("@UserId", user.UserId);
                SetParameters(user);
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

        public void DeleteLogically(User user)
        {
            try
            {
                _dataAccess.SetProcedure("SP_Delete_User_Logically");
                _dataAccess.SetParameter("@UserId", user.UserId);
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

        public void Delete(User user, bool isLogicalDeletion = true)
        {
            if (isLogicalDeletion == true)
            {
                DeleteLogically(user);
                return;
            }

            try
            {
                _dataAccess.SetQuery("delete from Users where UserId = @UserId");
                _dataAccess.SetParameter("@UserId", user.UserId);
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

        public int GetId(int personId)
        {
            int userId = 0;

            try
            {
                _dataAccess.SetProcedure("SP_Get_User_Id");
                _dataAccess.SetParameter("@PersonId", personId);
                userId = _dataAccess.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _dataAccess.CloseConnection();
            }

            return userId;
        }

        private void SetParameters(User user)
        {
            _dataAccess.SetParameter("@Username", user.Username);
            _dataAccess.SetParameter("@UserPassword", user.Password);
            _dataAccess.SetParameter("@RoleId", user.Role.Id);
            _dataAccess.SetParameter("@PersonId", user.PersonId);
        }

        public bool Login(User user)
        {
            try
            {
                _dataAccess.SetQuery(
                    "select UserId,RoleId,P.LastName, P.FirstName,P.Phone, P.Birth,  P.Email, P.TaxCode, P.AddressId, A.StreetName, A.StreetNumber, A.Flat, C.ZipCode,C.CityName,   U.Username from Users U Inner join People P on U.PersonId = P.PersonId Inner join Addresses A On A.AddressId = P.AddressId Inner Join Cities C ON A.CityId = C.CityId where P.Email = @Email AND U.UserPassword = @UserPassword"
                );
                _dataAccess.SetParameter("@Email", user.Email);
                _dataAccess.SetParameter("@UserPassword", user.Password);

                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    user.UserId = (int)_dataAccess.Reader["UserId"];
                    user.Role.Id = Convert.ToInt32(_dataAccess.Reader["RoleId"]);
                    user.LastName = (string)_dataAccess.Reader["LastName"];
                    user.FirstName = (string)_dataAccess.Reader["FirstName"];
                    user.Phone = (string)_dataAccess.Reader["Phone"];
                    user.TaxCode = (string)_dataAccess.Reader["TaxCode"];
                    user.Birth = DateTime.Parse(_dataAccess.Reader["Birth"].ToString());
                    user.Address.StreetName = (string)_dataAccess.Reader["StreetName"];
                    user.Address.StreetNumber = (string)_dataAccess.Reader["StreetNumber"];
                    user.Address.City.Name = (string)_dataAccess.Reader["CityName"];
                    user.Address.City.ZipCode = (string)_dataAccess.Reader["ZipCode"];
                    user.Address.Flat = (string)_dataAccess.Reader["Flat"];

                    return true;
                }
                return false;
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

        public bool IsAdmin(User user)
        {
            if (user.Role.Name == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
