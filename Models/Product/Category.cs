using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Product
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Не указана категория")]
        [MinLength(5)]
        [MaxLength(300)]
        public string Name { get; set; }
        public List<ManufacturerCategory> ManufacturerCategories { get; set; }
        public List<Product> Products { get; set; }
    }
}
