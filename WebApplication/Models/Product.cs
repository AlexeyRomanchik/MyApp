using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;


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
        public decimal Price { get; set; }

        [Display(Name = "Количество на складе")]
        [Required(ErrorMessage = "Не указано количество")]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Дата добавления")]
        [Required(ErrorMessage = "Не указана дата добавления товара")]
        public DateTime DateAdded { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [JsonIgnore]
        public Manufacturer Manufacturer { get; set; }

        [JsonIgnore]
        public List<Rating> Ratings { get; set; }

        [JsonIgnore]
        public List<Review> Reviews { get; set; }

        public bool IsAvailable() => 0 < QuantityInStock;

        public bool IsIndicatedRating()
        {
            if (Ratings == null)
                return false;
            return Ratings.Count > 0;
        }

        public int NumberOfRatings()
        {
            return Ratings?.Count ?? 0;
        }

        public double GetAverageRating()
        {
            if (Ratings == null)
                return 0;
            if (Ratings.Count < 1)
                return 0;
            return (double)Ratings.Sum(x => x.Value) / Ratings.Count;
        }
            

    }
}
