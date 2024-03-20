using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem;


namespace InventoryManagementSystem
{
    class UserInterface
    {
        private InventoryManagement inventoryManagement;
        private OrderManagement orderManagement;

        public UserInterface(InventoryManagement inventoryManagement)
        {
            this.inventoryManagement = inventoryManagement;
            this.orderManagement = new OrderManagement();
        }

        public void Start()
        {
            bool exit = false;
            while (!exit)
            {
                DisplayMainMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        UpdateStockQuantity();
                        break;
                    case "3":
                        PlaceOrder();
                        break;
                    case "4":
                        ViewProductDetails();
                        break;
                    case "5":
                        ViewOrderHistory();
                        break;
                    case "6":
                        RetrieveOrderDetails();
                        break;
                    case "7":
                        SearchProductsByName();
                        break;

                    case "8":
                        ViewAllProducts();
                        break;

                    case "9":
                        exit = true;
                        break;


                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Add New Product");
            Console.WriteLine("2. Update Stock Quantity");
            Console.WriteLine("3. Place Order");
            Console.WriteLine("4. View Product Details");
            Console.WriteLine("5. View Order History");
            Console.WriteLine("6. Retrieve Order Details");
            Console.WriteLine("7. Search Products by Name");
            Console.WriteLine("8. View all Products");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice: ");
        }

        private void AddProduct()
        {
            try
            {
                Console.WriteLine("Enter details for the new product:");

                Console.Write("Product ID: ");
                int productId;
                while (true)
                {
                    if (!int.TryParse(Console.ReadLine(), out productId))
                    {
                        Console.WriteLine("Invalid product ID. Please enter a valid integer value.");
                        Console.Write("Product ID: ");
                        continue;
                    }

                   
                    Product existingProduct = inventoryManagement.GetProductDetails(productId);
                    if (existingProduct != null)
                    {
                        Console.WriteLine($"Product with ID {productId} already exists. Please enter a different ID.");
                        Console.Write("Product ID: ");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.Write("Product Name: ");
                string? productName = Console.ReadLine();

                Console.Write("Category: ");
                string? category = Console.ReadLine();

                Console.Write("Price In MWK: ");
                decimal price;
                while (!decimal.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("Invalid price. Please enter a valid decimal value.");
                    Console.Write("Price: ");
                }

                Console.Write("Stock Quantity: ");
                int stockQuantity;
                while (!int.TryParse(Console.ReadLine(), out stockQuantity))
                {
                    Console.WriteLine("Invalid stock quantity. Please enter a valid integer value.");
                    Console.Write("Stock Quantity: ");
                }

                
                Product newProduct = new Product
                {
                    productId = productId,
                    ProductName = productName,
                    category = category,
                    price = price,
                    stockQuantity = stockQuantity
                };

                
                inventoryManagement.AddProduct(newProduct);

                Console.WriteLine("New product added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encountered an exception while adding product.");
                Console.WriteLine(ex.Message);
            }
        }


        private void UpdateStockQuantity()
        {
            Console.Write("Update Stock Quantity: ");
            Console.Write("Enter the product ID: ");
            int productId;
            while (!int.TryParse(Console.ReadLine(), out productId))
            {
                Console.WriteLine("Invalid product ID. Please enter a valid integer value.");
                Console.Write("Product ID: ");
            }

            Product? product = inventoryManagement.GetProductDetails(productId);

            if (product != null)
            {
                Console.WriteLine($"Current stock quantity for {product.ProductName}: {product.stockQuantity}");

                Console.Write("Enter the new stock quantity: ");
                int newStockQuantity;
                while (!int.TryParse(Console.ReadLine(), out newStockQuantity))
                {
                    Console.WriteLine("Invalid stock quantity. Please enter a valid integer value.");
                    Console.Write("New Stock Quantity: ");
                }

                
                inventoryManagement.UpdateStockQuantity(productId, newStockQuantity);

                Console.WriteLine($"Stock quantity updated successfully for {product.ProductName}.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        private void PlaceOrder()
        {
            try
            {
                Console.WriteLine("Enter details for the new order:");

                
                Console.Write("Order ID: ");
                int orderId;
                while (!int.TryParse(Console.ReadLine(), out orderId))
                {
                    Console.WriteLine("Invalid order ID. Please enter a valid integer value.");
                    Console.Write("Order ID: ");
                }

                Console.Write("Customer Name: ");
                string? customerName = Console.ReadLine();

                DateTime orderDate = DateTime.Now; 

                
                List<ProductQuantity> orderedProducts = new List<ProductQuantity>();

                Console.WriteLine("Enter the products and quantities (press Enter twice to finish):");
                while (true)
                {
                    Console.Write("Product ID: ");
                    int productId;
                    if (!int.TryParse(Console.ReadLine(), out productId))
                    {
                        Console.WriteLine("Invalid product ID. Please enter a valid integer value.");
                        continue;
                    }

                    Console.Write("Quantity: ");
                    int quantity;
                    if (!int.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.WriteLine("Invalid quantity. Please enter a valid integer value.");
                        continue;
                    }

                    orderedProducts.Add(new ProductQuantity { ProductId = productId, Quantity = quantity });

                    Console.Write("Press Enter to add another product or press Enter twice to finish: ");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                        Console.WriteLine();
                    }
                }

                
                Order newOrder = new Order
                {
                    OrderId = orderId,
                    CustomerName = customerName,
                    OrderDate = orderDate, 
                    OrderedProducts = orderedProducts
                };

                
                orderManagement.PlaceOrder(newOrder, inventoryManagement);

                Console.WriteLine("Order placed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encountered an exception while placing order: {ex.Message}");
            }
        }


        private void ViewProductDetails()
        {
            try
            {
                Console.WriteLine("Enter the Product ID to view details:");
                Console.Write("Product ID: ");
                int productId;
                while (!int.TryParse(Console.ReadLine(), out productId))
                {
                    Console.WriteLine("Invalid product ID. Please enter a valid integer value.");
                    Console.Write("Product ID: ");
                }

                
                Product? product = inventoryManagement.GetProductDetails(productId);

                if (product != null)
                {
                    Console.WriteLine("Product Details:");
                    Console.WriteLine($"ID: {product.productId}");
                    Console.WriteLine($"Name: {product.ProductName}");
                    Console.WriteLine($"Category: {product.category}");
                    Console.WriteLine($"Price: {product.price}");
                    Console.WriteLine($"Stock Quantity: {product.stockQuantity}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encountered an exception while viewing product details: {ex.Message}");
            }
        }

        private void ViewOrderHistory()
        {
            try
            {
                
                OrderManagement orderManagement = new OrderManagement();

                
                List<Order> allOrders = orderManagement.GetAllOrders();

                if (allOrders.Count > 0)
                {
                    Console.WriteLine("Order History:");
                    foreach (var order in allOrders)
                    {
                        Console.WriteLine($"Order ID: {order.OrderId}");
                        Console.WriteLine($"Customer Name: {order.CustomerName}");
                        Console.WriteLine($"Order Date: {order.OrderDate}");
                        Console.WriteLine("Ordered Products:");
                        foreach (var productQuantity in order.OrderedProducts)
                        {
                            Console.WriteLine($"Product ID: {productQuantity.ProductId}, Quantity: {productQuantity.Quantity}");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No orders found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encountered an exception while viewing order history: {ex.Message}");
            }
        }

        private void RetrieveOrderDetails()
        {
            Console.Write("Enter the order ID: ");
            int orderId;
            if (!int.TryParse(Console.ReadLine(), out orderId))
            {
                Console.WriteLine("Invalid order ID. Please enter a valid integer value.");
                return;
            }

            
            Order? order = orderManagement.GetOrderDetails(orderId);
            if (order != null)
            {
                
                Console.WriteLine($"Order ID: {order.OrderId}");
                Console.WriteLine($"Customer Name: {order.CustomerName}");
                Console.WriteLine($"Order Date: {order.OrderDate}");
                Console.WriteLine("Ordered Products:");
                foreach (var productQuantity in order.OrderedProducts)
                {
                    Console.WriteLine($"Product ID: {productQuantity.ProductId}, Quantity: {productQuantity.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }

        private void SearchProductsByName()
        {
            try
            {
                Console.Write("Enter partial name to search: ");
                string? partialName = Console.ReadLine();

               
                List<Product> foundProducts = inventoryManagement.SearchProductsByName(partialName);


                if (foundProducts.Count > 0)
                {
                    Console.WriteLine("Found Products:");
                    foreach (var product in foundProducts)
                    {
                        Console.WriteLine($"Product ID: {product.productId}, Product Name: {product.ProductName}");
                    }
                }
                else
                {
                    Console.WriteLine("No products found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching products by name: {ex.Message}");
            }
        }
        private void ViewAllProducts()
        {
            try
            {
                inventoryManagement.ViewAllProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while viewing all products: {ex.Message}");
            }
        }



    }
}
