using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace BestBuyBestPractice
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");


            IDbConnection conn = new MySqlConnection(connString);

            //Department
            var repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Enter a new Department name");

            var newDepartment = Console.ReadLine();

            foreach (var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }


            //Create Product 
            Console.WriteLine("What is the name of the new product?");
            var prodName = Console.ReadLine();

            Console.WriteLine("What is the price?");
            var prodPrice = double.Parse(Console.ReadLine());

            Console.WriteLine("What is the Category ID?");
            var prodCat = int.Parse(Console.ReadLine());

            var productRepo = new DapperProductRepository(conn);
            productRepo.CreateProducts(prodName, prodPrice, prodCat);

            var prodList = productRepo.GetAllProducts();

            foreach (var prod in prodList)
            {
                Console.WriteLine($"{prod.ProductID} - {prod.Name}");
            }

            //Update Product
            Console.WriteLine("What is the product ID you want to update?");
            var prodID = Console.ReadLine();

            Console.WriteLine(" what is the products new name?");
            var prodNewName = Console.ReadLine();

            productRepo.UpdateProductName(prodID, prodNewName);
            var prodList = productRepo.GetAllProducts();
            foreach (var prod in prodList)
            {
                Console.WriteLine($"{prod.ProductID} - {prod.Name} - {prod.Price}");
            }
        }
    }
}
