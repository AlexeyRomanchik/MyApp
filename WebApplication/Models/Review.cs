using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Display(Name = "Отзыв")]
        [Required(ErrorMessage = "Не указан отзыв")]
        [MinLength(20)]
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool ReviewVerified { get; set; }
        public string UserId { get; set; }
        public Guid ProductId { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
