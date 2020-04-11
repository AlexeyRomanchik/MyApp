﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название продукта")]
        [MinLength(5)]
        [MaxLength(300)]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Не указана цена продукта")]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Display(Name = "Количество на складе")]
        [Required(ErrorMessage = "Не указано количество")]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }
        public string ImageUrl { get; set; }
        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public List<Rating> Ratings { get; set; }

        public bool IsAvailable() => 0 < QuantityInStock;

    }
}