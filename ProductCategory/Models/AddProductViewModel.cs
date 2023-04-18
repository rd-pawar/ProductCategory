using ProductCategory.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductCategory.Models
{
    public class AddProductViewModel
    {
       

        
        public string? ProductName { get; set; }

        
        //public virtual Guid CategoryId { get; set; }

       
        public virtual string Categories { get; set; }
        public List<string>? LCategory { get; set; }



    }
}
