using System.ComponentModel;
using System.Collections.Generic;

namespace DomainModelLayer
{
    public class User : Person
    {
        [DisplayName("ID")]
        public int UserId { get; set; }

        [DisplayName("Nombre de usuario")]
        public string Username { get; set; }

        [DisplayName("Contraseña")]
        public string Password { get; set; }

        [DisplayName("Roles")]
        public List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                return Username;
            }
            else
            {
                return base.ToString();
            }
        }
    }
}
