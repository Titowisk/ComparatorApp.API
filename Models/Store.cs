using System.Collections.Generic;

namespace ComparatorApp.API.Models
{
    public class Store
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public List<ItemDetail> ItemsDetail { get; set; }
    }
}