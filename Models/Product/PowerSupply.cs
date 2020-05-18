using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Product
{
    public class PowerSupply
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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

        public Product Product { get; set; }
        public List<PowerSupplyInterface> PowerSupplyInterfaces { get; set; }
    }
}
