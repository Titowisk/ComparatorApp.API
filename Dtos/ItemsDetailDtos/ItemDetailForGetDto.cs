using System;

namespace ComparatorApp.API.Dtos.ItemsDetailDtos
{
    public class ItemDetailForGetDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        // public Item Item { get; set; }
        public string ItemName { get; set; }
        public int BrandId { get; set; }
        // public Brand Brand { get; set; }
        public string BrandName { get; set; }
        public int StoreId { get; set; }
        // public Store Store { get; set; }
        public string StoreName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public int BaseUnitId { get; set; }
        // public BaseUnit BaseUnit { get; set; }
        public string BaseUnitName { get; set; }
        public DateTime Created { get; set; }
    }
}