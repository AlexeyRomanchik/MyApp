﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public List<CartItem> CartItems { get; set; }

        [Display(Name = "Итоговая цена")]
        [Required(ErrorMessage = "Не указана итоговая цена")]
        public double FinalPrice { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
            FinalPrice = 0;
        }

        public double GetFinalPrice()
        {
            double finalPrice = 0;

            if(CartItems == null) return finalPrice;

            foreach (var cartItem in CartItems)
            {
                finalPrice += cartItem.Product.Price * cartItem.Quantity;
            }

            return finalPrice;
        }

        public int GetTotalItemsCount()
        {
            var itemsCount = 0;

            foreach (var item in CartItems)
            {
                itemsCount += item.Quantity;
            }

            return itemsCount;
        }

    }
}