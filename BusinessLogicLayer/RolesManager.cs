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
            // hack : No es necesario para un MVP
            return -1;
        }

        public void Edit(Role role)
        {
            // hack : No es necesario para un MVP
        }

        public int GetId(Role role)
        {
            int roleId = 0;

            try
            {
                _dataAccess.SetQuery("select RoleId from Roles where RoleName = @RoleName");
                _dataAccess.SetParameter("@RoleName", role.Name);
                _dataAccess.ExecuteRead();

                if (_dataAccess.Reader.Read())
                {
                    roleId = (int)_dataAccess.Reader["RoleId"];
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

            return roleId;
        }
    }
}
