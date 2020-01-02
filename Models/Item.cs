using System;
using System.Collections.Generic;

namespace ComparatorApp.API.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemDetail> ItemsDetail { get; set; }
    }
}