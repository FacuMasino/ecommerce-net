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
        private User _user;

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

            user.Role = ReadRole(user.Role.Id);

            _person = _peopleManager.Read(user.PersonId);
            Helper.AssignPerson(user, _person);

            return user;
        }

        public int GetUserId(int personId)
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

        private Role ReadRole(int roleId)
        {
            Role role = new Role();

            try
            {
                _dataAccess.SetQuery("select RoleName from Roles where RoleId = @RoleId");
                _dataAccess.SetParameter("@RoleId", roleId);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    role.Id = roleId;
                    role.Name = _dataAccess.Reader["RoleName"]?.ToString();
                    role.Name = role.Name ?? "";
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

            return role;
        }

        public bool Login(User user)
        {
            try
            {
                _dataAccess.SetQuery(
                    "select UserId,RoleId from Users U Inner join People P on U.PersonId = P.PersonId  where P.Email = @Email AND U.UserPassword = @Pass"
                );
                _dataAccess.SetParameter("@Email", user.Email);
                _dataAccess.SetParameter("@Pass", user.Password);

                _dataAccess.ExecuteRead();

                while (_dataAccess.Reader.Read())
                {
                    user.UserId = (int)_dataAccess.Reader["UserId"];
                    user.Role.Id = Convert.ToInt32(_dataAccess.Reader["RoleId"]);
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
    }
}
