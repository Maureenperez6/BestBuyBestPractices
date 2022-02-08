using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyBestPractice
{
    public interface IProductRepository
    {
        public IEnumerable<IProductRepository> GetAllProducts();

        public void CreateProducts(string name, double price, int CategoryID);
    }
}
