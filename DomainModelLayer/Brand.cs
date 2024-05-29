namespace DomainModelLayer
{
    public class Brand
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public Brand()
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
