﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket(string Id)
        {
            this.Id = Id;
        }
        public string Id { get; set; } = null!;
        public List<BasketItem> Items { get; set; } 
    }
}
