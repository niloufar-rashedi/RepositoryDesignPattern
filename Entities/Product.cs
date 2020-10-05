using System;
using System.Collections.Generic;
using System.Text;

namespace Uppgift4
{
    public class Product
    {
        public Product()
        { }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public Shop Shop { get; set; }

        public Manufacturer Manu; 

        public static explicit operator Product(List<Product> v)
        {
            throw new NotImplementedException();
        }
    }
}
