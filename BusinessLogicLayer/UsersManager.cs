using System;
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
        private AddressesManager _addressesManager = new AddressesManager();
        private Address _address;

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

            _rolesManager.AssignUserRole(user);

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
                _dataAccess.SetProcedure("SP_Get_User_Id_By_Person");
                _dataAccess.SetParameter("@PersonId", personId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    userId = (int)_dataAccess.Reader["UserId"];
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

            return userId;
        }

        public int GetId(string email, string password)
        {
            int userId = 0;

            try
            {
                _dataAccess.SetProcedure("SP_Get_User_Id_By_Credentials");
                _dataAccess.SetParameter("@Email", email);
                _dataAccess.SetParameter("@UserPassword", password);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    userId = (int)_dataAccess.Reader["UserId"];
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

            return userId;
        }

        public bool Login(User user)
        {
            int userId = GetId(user.Email, user.Password);

            if (userId == 0)
            {
                return false;
            }

            User aux = Read(userId);
            Helper.AssignPerson<User, User>(user, aux);

            return true;
        }

        public bool IsAdmin(User user)
        {
            if (user.Role.Id == (int)RolesManager.Roles.AdminRoleId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetParameters(User user)
        {
            _dataAccess.SetParameter("@Username", user.Username);
            _dataAccess.SetParameter("@UserPassword", user.Password);
            _dataAccess.SetParameter("@RoleId", user.Role.Id);
            _dataAccess.SetParameter("@PersonId", user.PersonId);
        }
    }
}
