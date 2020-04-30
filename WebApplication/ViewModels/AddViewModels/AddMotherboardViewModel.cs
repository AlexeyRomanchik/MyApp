using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels.AddViewModels
{
    public class AddMotherboardViewModel : AddProductViewModel
    {
        [Display(Name = "Чипсет")]
        [Required(ErrorMessage = "Поле обязательное")]
        public string ChipSet { get; set; }

        [Display(Name = "Форм-фактор")]
        [Required(ErrorMessage = "Поле обязательное")]
        public string FormFactor { get; set; }

        [Display(Name = "Количество слотов оперативной патями")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int MemorySlotsNumber { get; set; }
    }
}
