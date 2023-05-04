﻿namespace BackendApi.Contracts.Product
{
    public class GetProductRequest
    {
        public int NumberProduct { get; set; }
        public int IdCategories { get; set; }
        public string Namee { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; } = null!;
        public string Article { get; set; } = null!;
    }
}
