namespace DomainModelLayer
{
    public class Category
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public Category()
        {
            Description = "";
        }

        public override string ToString()
        {
            if (Description != null)
            {
                return Description;
            }
            else
            {
                return "";
            }
        }
    }
}
