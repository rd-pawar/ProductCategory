using System.ComponentModel.DataAnnotations;

namespace ProductCategory.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


    }
}
