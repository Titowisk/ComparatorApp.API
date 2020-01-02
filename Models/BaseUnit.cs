using System.Collections.Generic;

namespace ComparatorApp.API.Models
{
    public class BaseUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public List<ItemDetail> ItemsDetail { get; set; }
    }
}