﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Product
{
    public class Motherboard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ChipSet { get; set; }
        public string FormFactor { get; set; }
        public int RamMemoryTypeId { get; set; }
        public int MemorySlotsNumber { get; set; }

        public Product Product { get; set; }

        public List<MotherboardInterface> MotherboardInterfaces { get; set; }
        public SocketType SocketType { get; set; }

    }
}
