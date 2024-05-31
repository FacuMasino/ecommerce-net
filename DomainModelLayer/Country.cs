using System.ComponentModel;

namespace DomainModelLayer
{
    public class Country
    {
        // PROPERTIES

        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("País")]
        public string Name { get; set; }

        // METHODS

        public override string ToString()
        {
            return Name;
        }
    }
}
