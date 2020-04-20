using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class GraphicsCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Частота графического процессора (МГц)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double GpuFrequency { get; set; }

        [Display(Name = "Turbo-частота графического процессора (МГц)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double GpuTurboFrequency { get; set; }

        [Display(Name = "Количество потоковых процессоров")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int StreamProcessorsNumber { get; set; }

        [Display(Name = "Видеопамять (GB)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int VideoMemory { get; set; }

        [Display(Name = "Частота памяти")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double MemoryFrequency { get; set; }

        [Display(Name = "Ширина шины памяти (bit)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int MemoryBusWidth { get; set; }

        [Display(Name = "Поддержка DirectX")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int DirectXSupport { get; set; }

        [Display(Name = "Рекомендуемый блок питания (Вт)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int RecommendedPowerSupply { get; set; }

        [Display(Name = "Количество вентиляторов")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int FansNumber { get; set; }

        public int GraphicsCardMemoryTypeId { get; set; }

        public Product Product { get; set; }
        public GraphicsCardMemoryType MemoryType { get; set; }

        public int InterfaceId { get; set; }
        public Interface Interface { get; set; }
    }
}
