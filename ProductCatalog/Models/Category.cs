using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();

    }
}
