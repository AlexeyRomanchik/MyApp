﻿namespace Models.Product
{
    public class ManufacturerCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
