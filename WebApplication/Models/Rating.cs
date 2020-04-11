﻿using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication.Models
{
    public class Rating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int Value { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
