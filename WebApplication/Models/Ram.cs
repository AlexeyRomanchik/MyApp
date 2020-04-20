using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Ram
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int RamMemoryTypeId { get; set; }

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

        public int InterfaceId { get; set; }
        public RamMemoryType MemoryType { get; set; }
        public Product Product { get; set; }
        public Interface Interface { get; set; }
    }
}
