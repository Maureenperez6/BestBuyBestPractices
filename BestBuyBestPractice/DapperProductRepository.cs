using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyBestPractice
{
    public class DapperProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateProducts(string name, double price, int CategoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, ProductID) " +
                "Values (@name, @price, @categoryID);",
                new { name = name, price = price, CategoryID = CategoryID });
        }

        public IEnumerable<IProductRepository> GetAllProducts()
        {
            return _connection.Query<Products>("SELECT * FROM products;");
        }

        public void UpdateProduct(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
               new { productID = productID, updatedName = updatedName });
            Console.WriteLine();
            Console.WriteLine($"Product #{productID} updated");
            Thread.Sleep(3000);
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });
            Console.WriteLine();
            Console.WriteLine($"Product# {productID} was deleted.");
            Thread.Sleep(3000)
;

            var proRepo = new DapperProductRepository(_connection);
            var prodList = prodRepo.GetAllProducts();
            foreach (var prod in prodList)
            {
                Console.WriteLine($"{prod.ProductID} - {prod.Name} - {prod.Price}");
            }

            Console.WriteLine("What is the product ID you want to Delete?");
            var prodID = int.Parse(Console.ReadLine());
            productRepo.DeleteProduct(prodID);

        }

    }
}
