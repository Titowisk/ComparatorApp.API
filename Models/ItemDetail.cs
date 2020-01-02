using System;

namespace ComparatorApp.API.Models
{
    public class ItemDetail
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public int BaseUnitId { get; set; }
        public BaseUnit BaseUnit { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}