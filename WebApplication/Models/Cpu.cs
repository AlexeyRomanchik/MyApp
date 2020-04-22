using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Cpu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Сокет")]
        [Required(ErrorMessage = "Поле обязательное")]
        public string Socket { get; set; }

        [Display(Name = "Количество ядер")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int NumberCores { get; set; }

        [Display(Name = "Максимальное количество потоков")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int MaximumNumberThreads { get; set; }

        [Display(Name = "Базовая тактовая частота")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double BaseClock { get; set; }

        [Display(Name = "Максимальная частота")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double MaximumFrequency { get; set; }

        [Display(Name = "Кэш L2 (MB)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double L2Cache { get; set; }

        [Display(Name = "Кэш L3 (MB)")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double L3Cache { get; set; }

        [Display(Name = "Поддержка памяти")]
        [Required(ErrorMessage = "Поле обязательное")]
        public string MemorySupport { get; set; }

        [Display(Name = "Количество каналов памяти")]
        [Required(ErrorMessage = "Поле обязательное")]
        public int NumberMemoryChannels { get; set; }

        [Display(Name = "Максимальная частота памяти")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double MaxMemoryFrequency { get; set; }

        [Display(Name = "Расчетная тепловая мощность Вт")]
        [Required(ErrorMessage = "Поле обязательное")]
        public double Tdp { get; set; }

        public int SocketTypeId { get; set; }

        public Product Product { get; set; }
        public SocketType SocketType { get; set; }

    }
}
