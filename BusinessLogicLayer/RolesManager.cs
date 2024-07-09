using System;
using DataAccessLayer;
using DomainModelLayer;

namespace BusinessLogicLayer
{
    public class RolesManager
    {
        private DataAccess _dataAccess = new DataAccess();

        public enum Roles
        {
            AdminRoleId = 1,
            DefaultRoleId = 2,
            VisitorRoleId = 3
        }

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

        public void AssignUserRole(User user)
        {
            if (user.Role == null)
            {
                user.Role = new Role { Id = (int)Roles.DefaultRoleId };
                return;
            }

            if (!string.IsNullOrEmpty(user.Role.Name))
            {
                int foundRoleId = GetId(user.Role);
                user.Role.Id = foundRoleId != 0 ? foundRoleId : (int)Roles.DefaultRoleId;
            }
            else
            {
                user.Role.Id = (int)Roles.DefaultRoleId;
            }
        }
    }
}
