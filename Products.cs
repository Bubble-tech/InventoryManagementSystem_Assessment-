using InventoryManagementSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;



//class 1: product class
public class Product
{
    public int productId { get; set; }
    public  string? ProductName { get; set; }
    public  string? category { get; set; }
    public decimal price { get; set; }
    public int stockQuantity { get; set; }

}






