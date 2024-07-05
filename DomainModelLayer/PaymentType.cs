using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelLayer
{
    public class PaymentType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            if (Name != null)
            {
                return Name;
            }
            else
            {
                return "";
            }
        }
    }
}
