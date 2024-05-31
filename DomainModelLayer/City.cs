using System.ComponentModel;

namespace DomainModelLayer
{
    public class City
    {
        // PROPERTIES

        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Ciudad")]
        public string Name { get; set; }

        [DisplayName("Código Postal")]
        public string ZipCode { get; set; }

        // METHODS

        public override string ToString()
        {
            return Name;
        }
    }
}
