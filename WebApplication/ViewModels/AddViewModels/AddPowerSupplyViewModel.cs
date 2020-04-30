using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels.AddViewModels
{
    public class AddPowerSupplyViewModel : AddProductViewModel
    {
        [Display(Name = "Мощность (Вт)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double Power { get; set; }

        [Display(Name = "Стандарт")]
        [Required(ErrorMessage = "Поле обязательное")]
        public string Standard { get; set; }

        [Display(Name = "Количество отдельных линий")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int NumberIndividualLines { get; set; }

        [Display(Name = "Максимальный ток по линии (V)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double MaxLineCurrent { get; set; }

        [Display(Name = "Коррекция фактора мощности (А)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double PowerFactorCorrection { get; set; }

        [Display(Name = "Активный КПД (%)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double ActiveEfficiency { get; set; }

        [Display(Name = "Размер вентилятора блока питания (мм)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int FanSize { get; set; }

        [Display(Name = "Количество вентиляторов")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int FansNumber { get; set; }
    }
}
