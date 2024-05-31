using System.ComponentModel;

namespace DomainModelLayer
{
    public class Province
    {
        // PROPERTIES

        [DisplayName("ID de provincia")]
        public int ProvinceId { get; set; }

        [DisplayName("Provincia")]
        public string Name { get; set; }

        // METHODS

        public override string ToString()
        {
            return Name;
        }
    }
}
