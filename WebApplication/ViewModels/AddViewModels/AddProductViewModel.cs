using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebApplication.ViewModels.AddViewModels
{
    public class AddProductViewModel
    {

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название продукта")]
        [MinLength(5)]
        [MaxLength(300)]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Не указана цена продукта")]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Количество на складе")]
        [Required(ErrorMessage = "Не указано количество")]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        public IFormFile UploadedFile { get; set; }
    }
}
