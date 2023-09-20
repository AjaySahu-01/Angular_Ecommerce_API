using System.ComponentModel.DataAnnotations;

namespace Angular_Ecommerce.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string OriginalPrice { get; set; }
        public string SellingPrice { get; set; }
        public string Img { get; set; }
    }
}
