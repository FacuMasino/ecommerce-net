using DataAccessLayer;
using DomainModelLayer;
using System;

namespace BusinessLogicLayer
{
    public class RolesManager
    {
        private DataAccess _dataAccess = new DataAccess();

        public Role Read(int roleId)
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

        public int Add(Role role)
        {
            return -1;
        }

        public void Edit(Role role)
        {

        }

        public int GetId(Role role)
        {
            return -1;
        }
    }
}
