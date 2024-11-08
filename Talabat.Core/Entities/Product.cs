﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!; 
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; } 

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
