﻿namespace Product.Application.Dtos.Product
{
    public class RelatedProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string MainImageUrl { get; set; }
    }
}