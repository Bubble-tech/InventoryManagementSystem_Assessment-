using InventoryManagementSystem;
using Newtonsoft.Json;

class OrderManagement
{
    private List<Order> Orders { get; set; }
    private string OrdersFilePath = "orders.json";

    public OrderManagement()
    {
        Orders = new List<Order>();
        LoadOrders(); 
    }

    public void PlaceOrder(Order newOrder, InventoryManagement inventoryManagement)
    {
        
        foreach (var productQuantity in newOrder.OrderedProducts)
        {
            foreach (var product in inventoryManagement.Products)
            {
                if (product.productId == productQuantity.ProductId)
                {
                    product.stockQuantity -= productQuantity.Quantity;
                    break; 
                }
            }
        }

        
        Orders.Add(newOrder);

        
        SaveOrders();
    }

    public Order? GetOrderDetails(int orderId)
    {
        foreach (var order in Orders)
        {
            if (order.OrderId == orderId)
            {
                return order;
            }
        }
        return null; 
    }

    private bool SaveOrders()
    {
        try
        {
            string json = JsonConvert.SerializeObject(Orders, Formatting.Indented);
            File.WriteAllText(OrdersFilePath, json);
            return true; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while saving orders: {ex.Message}");
            return false; 
        }
    }

    private void LoadOrders()
    {
        if (File.Exists(OrdersFilePath))
        {
            string json = File.ReadAllText(OrdersFilePath);
            Orders = JsonConvert.DeserializeObject<List<Order>>(json);
        }
    }
    public List<Order> GetAllOrders()
    {
        return Orders;
    }
}
