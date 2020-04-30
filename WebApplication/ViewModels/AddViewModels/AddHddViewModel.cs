using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels.AddViewModels
{
    public class AddHddViewModel : AddProductViewModel
    {
        [Display(Name = "Объём")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int Volume { get; set; }

        [Display(Name = "Форм-фактор")]
        [Required(ErrorMessage = "Поле обязательное")]
        public string FormFactor { get; set; }

        [Display(Name = "Скорость вращения шпинделя")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double SpindleSpeed { get; set; }

        [Display(Name = "Буфер")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int Buffer { get; set; }

        [Display(Name = "Размер сектора")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int SectorSize { get; set; }

        [Display(Name = "Скорость последовательного чтения")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int SequentialReadSpeed { get; set; }

        [Display(Name = "Скорость последовательной записи")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int SequentialWriteSpeed { get; set; }

        [Display(Name = "Энергопотребление (Чтение/Запись)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double PowerConsumptionForReadWrite { get; set; }

        [Display(Name = "Энергопотребление (Ожидание)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double PowerConsumptionStandby { get; set; }

        [Display(Name = "Уровень шума (Чтение/Запись)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double NoiseReadingWriting { get; set; }

        [Display(Name = "Уровень шума (Ожидание)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double NoiseStandby { get; set; }
    }
}
