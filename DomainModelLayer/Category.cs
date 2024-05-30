namespace DomainModelLayer
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category()
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
