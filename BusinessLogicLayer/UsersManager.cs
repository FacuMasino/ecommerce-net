using System;
using System.Collections.Generic;
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

        public List<User> List()
        {
            List<User> users = new List<User>();

            try
            {
                _dataAccess.SetProcedure("SP_List_Users");
                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    User user= new User();

                    user.UserId = (int)_dataAccess.Reader["UserId"];
                    user.Username = _dataAccess.Reader["Username"]?.ToString();
                    user.Username = user.Username ?? "";
                    user.Password = (string)_dataAccess.Reader["UserPassword"];
                    user.PersonId = (int)_dataAccess.Reader["PersonId"];

                    users.Add(user);
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

            foreach (User user in users)
            {
                user.Roles = _rolesManager.List(user.UserId);

                _person = _peopleManager.Read(user.PersonId);
                Helper.AssignPerson(user, _person);
            }

            return users;
        }

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

            user.Roles = _rolesManager.List(user.UserId);

            _person = _peopleManager.Read(user.PersonId);
            Helper.AssignPerson(user, _person);

            return user;
        }

        public int Add(User user)
        {
            _peopleManager.HandlePersonId(user);
            _rolesManager.HandleRolesId(user.Roles);

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
            _rolesManager.HandleRolesId(user.Roles);

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

        public int GetPersonId(string email)
        {
            return _peopleManager.GetId(email);
        }

        public int GetUserId(int personId)
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

        public int GetUserId(string email, string password)
        {
            int userId = 0;

            try
            {
                _dataAccess.SetProcedure("SP_Get_User_Id_By_Credentials");
                _dataAccess.SetParameter("@Email", email);
                _dataAccess.SetParameter("@Password", password);
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
            int userId = GetUserId(user.Email, user.Password);

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
            return UserHasRole(user, (int)RolesManager.Ids.AdminRoleId);
        }

        public bool IsCustomer(User user)
        {
            return UserHasRole(user, (int)RolesManager.Ids.CustomerRoleId);
        }

        public bool UserHasRole(User user, Role role)
        {
            return UserHasRole(user, role.Id);
        }

        public bool UserHasRole(User user, int roleId)
        {
            for (int i = 0; i < user.Roles.Count; i++)
            {
                if (user.Roles[i].Id == roleId)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsBackOfficeUser(User user)
        {
            if (UserHasRole(user, (int)RolesManager.Ids.AdminRoleId))
            {
                return true;
            }

            if (UserHasRole(user, (int)RolesManager.Ids.DeliveryDriverRoleId))
            {
                return true;
            }

            if (UserHasRole(user, (int)RolesManager.Ids.CustomerServiceRoleId))
            {
                return true;
            }

            return false;
        }

        public void ToggleUserRole(User user, Role role)
        {
            try
            {
                _dataAccess.SetProcedure("SP_Toggle_User_Role");
                _dataAccess.SetParameter("@UserId", user.UserId);
                _dataAccess.SetParameter("@RoleId", role.Id);
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

        private void SetParameters(User user)
        {
            if (user.Username != null)
            {
                _dataAccess.SetParameter("@Username", user.Username);
            }
            else
            {
                _dataAccess.SetParameter("@Username", DBNull.Value);
            }
            
            _dataAccess.SetParameter("@UserPassword", user.Password);
            _dataAccess.SetParameter("@PersonId", user.PersonId);
        }
    }
}
