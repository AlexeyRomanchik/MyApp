using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels.AddViewModels
{
    public class AddRamViewModel : AddProductViewModel
    {
        [Display(Name = "Общий объем памяти")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int TotalMemory { get; set; }

        [Display(Name = "Тактовая частота")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double Frequency { get; set; }

        [Display(Name = "Количество контактов")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int ContactsNumber { get; set; }

        [Display(Name = "Пропускная способность")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double Throughput { get; set; }

        [Display(Name = "Напряжение питания")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double SupplyVoltage { get; set; }


    }
}
