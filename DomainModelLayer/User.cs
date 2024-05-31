using System.ComponentModel;

namespace DomainModelLayer
{
    internal class User : Person
    {
        [DisplayName("Nombre de usuario")]
        public string Username { get; set; }

        [DisplayName("Contraseña")]
        public string Password { get; set; }

        [DisplayName("Rol")]
        public Role Role { get; set; }
    }
}
