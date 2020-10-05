using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Uppgift4
{
    public interface IProductRepository
    {
        void Create();

        public ICollection<Product> GetAll();
        Product GetProductByName();
        void Insert();
        void Delete(/*Product product*/);   
        void Save(Product productChanged);
        void Search();
        void FilterPrice();//Filtererar produkter som kostar mindre än 50 
        void ManufacturerAndProducts();
    }
}
