using System.ComponentModel.DataAnnotations;

namespace Angular_Ecommerce.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Img { get; set; }
    }
}
