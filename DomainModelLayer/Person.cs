using System;
using System.ComponentModel;

namespace DomainModelLayer
{
    public class Person
    {
        [DisplayName("ID")]
        public int PersonId { get; set; }

        [DisplayName("Activo")]
        public bool IsActive { get; set; }

        [DisplayName("Nombre")]
        public string FirstName { get; set; }

        [DisplayName("Apellido")]
        public string LastName { get; set; }

        [DisplayName("DNI/CUIL/CUIT")]
        public string TaxCode { get; set; }

        [DisplayName("Teléfono")]
        public string Phone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Nacimiento")]
        public DateTime Birth { get; set; }

        [DisplayName("Domicilio")]
        public Address Address { get; set; }

        public Person()
        {
            Address = new Address();
            Birth = DateTime.Parse("1900/01/01");
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
            {
                return FirstName + " " + LastName;
            }
            else
            {
                return "";
            }
        }
    }
}
