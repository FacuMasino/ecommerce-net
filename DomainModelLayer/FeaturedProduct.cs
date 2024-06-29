using System.ComponentModel;

namespace DomainModelLayer
{
    public class FeaturedProduct : Product
    {
        [DisplayName("Orden")]
        public int DisplayOrder { get; set; }

        [DisplayName("Nuevo")]
        public bool ShowAsNew { get; set; }

        public FeaturedProduct()
        {
            ShowAsNew = false;
        }

        public FeaturedProduct(Product product)
        {
            Id = product.Id;
            Code = product.Code;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Cost = product.Cost;
            Stock = product.Stock;
            Brand = product.Brand;
            Categories = product.Categories;
            Images = product.Images;
            IsActive = product.IsActive;
        }
    }
}
