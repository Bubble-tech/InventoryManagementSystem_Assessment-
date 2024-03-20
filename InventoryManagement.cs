using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace InventoryManagementSystem
{
    public class InventoryManagement
    {

        public List<Product> Products;

        public InventoryManagement()
        {
            Products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
            SaveProducts(Products);
        }

        public void UpdateStockQuantity(int productId, int quantityChange)
        {
            bool productFound = false;
            foreach (var product in Products)
            {
                try
                {
                    if (product.productId == productId)
                    {
                        product.stockQuantity += quantityChange;
                        if (product.stockQuantity < 0)
                        {
                            Console.WriteLine("Stock not found");
                        }
                        else
                        {
                            SaveProducts(Products);
                        }
                        productFound = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while updating stock: {ex.Message}");
                }
            }


            if (!productFound)
            {
                Console.WriteLine("Product not found!");
            }
        }



        public Product? GetProductDetails(int productId)
        {
            if (Products.Count == 0)
            {
                // Load products from JSON file
                Products = LoadProducts();
            }

            foreach (var product in Products)
            {
                if (product.productId == productId)
                {
                    return product;
                }
            }

            return null;
        }

        //method 4: Listing All products 
        public List<Product> ListProductsByCategory(string category)
        {
            List<Product> matchedProducts = new List<Product>();
            foreach (var product in Products)

                if (product.category == category)
                {

                    matchedProducts.Add(product);

                }
            return matchedProducts;
        }
        //method 5 : calculating total value number of quantity in units and the price
        public decimal CalculateInventoryValue()
        {
            decimal amount = 0;
            foreach (var product in Products)
            {
                amount += product.price * product.stockQuantity;

            }
            return amount;

        }

        //method 6 : search product by name
        public List<Product> SearchProductByName(string name)
        {
            List<Product> searchResults = new List<Product>();
            string searchTerm = name.ToLower();
            foreach (var product in Products)
            {
                string productNameToLower = product.ProductName.ToLower();
                if (productNameToLower.StartsWith(searchTerm))
                {
                    searchResults.Add(product);
                }
            }
            return searchResults;
        }

        //loading data from json
        private List<Product> LoadProducts()
        {
            try
            {
                string filePath = @"C:\Users\Lenovo\source\repos\InventoryManagementSystem\InventoryManagementSystem\bin\Debug\net8.0\products.json"; // Replace this with the absolute file path
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Products file is missing.");
                    return new List<Product>();
                }

                string json = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(json))
                {
                    Console.WriteLine("Products file is empty.");
                    return new List<Product>();
                }

                List<Product>? productList = JsonConvert.DeserializeObject<List<Product>>(json);
                return productList ?? new List<Product>();
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine("Error deserializing products: {0}", ex.Message);
                return new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading products: {0}", ex.Message);
                return new List<Product>();
            }
        }

        public bool SaveProducts(List<Product> products)
        {
            try
            {

                string json = JsonConvert.SerializeObject(products, Formatting.Indented);

                if (json == null || json.Length == 0)
                {
                    return false;
                }

                File.WriteAllText("products.json", json);

                Console.WriteLine("Products saved successfully to products.json");
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error saving products: {ex.Message}");
                return false;
            }
        }
        public List<Product> SearchProductsByName(string partialName)
        {
            List<Product> foundProducts = new List<Product>();

            try
            {
                string json = File.ReadAllText("products.json");
                List<Product> allProducts = JsonConvert.DeserializeObject<List<Product>>(json);

                foreach (var product in allProducts)
                {
                    if (product.ProductName != null && product.ProductName.IndexOf(partialName, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        foundProducts.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching products by name: {ex.Message}");
            }

            return foundProducts;
        }

        public void ViewAllProducts()
        {
            try
            {
                string json = File.ReadAllText("products.json");
                List<Product> allProducts = JsonConvert.DeserializeObject<List<Product>>(json);

                Console.WriteLine("All Products:");
                foreach (var product in allProducts)
                {
                    Console.WriteLine($"Product ID: {product.productId}, Product Name: {product.ProductName}, Category: {product.category}, Price: {product.price}, Stock Quantity: {product.stockQuantity}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while viewing all products: {ex.Message}");
            }
        }
    }
}
