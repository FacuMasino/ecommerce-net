namespace DomainModelLayer
{
    public class Brand
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public Brand()
        {
            Name = "";
        }

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
