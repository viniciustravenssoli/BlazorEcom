﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();

        public Category? Category { get; set; }

        public int CategoryId { get; set; }
        
        public bool Featured { get; set; } = false;
    }
}
