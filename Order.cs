using System;
using System.Collections.Generic;
using InventoryManagementSystem; 

namespace InventoryManagementSystem
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ProductQuantity> OrderedProducts { get; set; } = new List<ProductQuantity>();
    }
}
