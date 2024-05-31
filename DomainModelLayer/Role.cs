using System.ComponentModel;

namespace DomainModelLayer
{
    internal class Role
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Rol")]
        public string Name { get; set; }
    }
}
