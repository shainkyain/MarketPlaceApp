﻿namespace MarketPlaceApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public decimal price { get; set; }
        public string ImageURL { get; set; }
    }
}