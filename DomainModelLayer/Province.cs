using System.ComponentModel;

namespace DomainModelLayer
{
    public class Province
    {
        // PROPERTIES

        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Provincia")]
        public string Name { get; set; }

        // METHODS

        public override string ToString()
        {
            return Name;
        }
    }
}
