using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using MinimumEditDistance;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using System.Text.RegularExpressions;
using Raven.Abstractions.Extensions;

namespace Uppgift4
{
    public class ProductRepository : IProductRepository
    {      
        public ProductRepository()
        {}
        #region Create metod där listan skapades och sparades som json fil
        public void Create()
        {
            Shop shop1 = new Shop() { ShopId = 1, ShopCity = "Borås" };
            Shop shop2 = new Shop() { ShopId = 2, ShopCity = "Göteborg" };
            Shop shop3 = new Shop() { ShopId = 3, ShopCity = "Lidköping" };

            var products = new List<Product>()
            {

                new Product(){ ProductName = "Carpet", Price = 1229.99,  Shop = shop1 , Manu =  new Manufacturer("ICA")},
                new Product(){ ProductName = "Shampoo", Price = 89.55,  Shop = shop1, Manu =  new Manufacturer("DEM")},
                new Product(){ ProductName = "Towel", Price = 51.25,  Shop = shop3, Manu =  new Manufacturer("DEM")},
                new Product(){ ProductName = "Tea bag", Price = 1.25,  Shop =shop1, Manu =  new Manufacturer("Hemkop")},
                new Product(){ ProductName = "Soap", Price = 0.99,  Shop = shop1, Manu =  new Manufacturer("DEM")},
                new Product(){ ProductName = "Shower Gel", Price = 27.60,  Shop = shop1, Manu =  new Manufacturer("DEM")},
                new Product(){ ProductName = "Tooth Brush", Price = 160.25,  Shop = shop3, Manu =  new Manufacturer("DEM")},
                new Product(){ ProductName = "Bread", Price = 8.0,  Shop = shop3, Manu =  new Manufacturer("Hemkop")},
                new Product(){ ProductName = "Home Appliences Full Pack", Price = 5999.99,  Shop = shop2, Manu =  new Manufacturer("ICA")},
                new Product(){ ProductName = "Headphone", Price = 268.99,  Shop = shop1, Manu =  new Manufacturer("ICA")},
                new Product(){ ProductName = "Tomato paste", Price = 9.99,  Shop = shop2, Manu =  new Manufacturer("DEM")},
            };

            if (products.Count == 0)
            {
                throw new ArgumentException("Your list has no member yet! Fill in the data");
            }
            else
            {
                string jsonFile = JsonConvert.SerializeObject(products, Formatting.Indented);
                Console.WriteLine(jsonFile);
                File.WriteAllText(@"products.json", jsonFile);
                Console.WriteLine();
                Console.WriteLine("************************************************************************");
                Console.WriteLine("The list is stored in the bin/Debug/netcoreapp3.1 folder of this project");
            }
        }
        #endregion
        private readonly string jsonFile = @"products.json";
        public ICollection<Product> GetAll()
        {
            List<Product> allProducts = new List<Product>();
            string getAll = File.ReadAllText(jsonFile);
            JsonConvert.PopulateObject(getAll, allProducts);
            foreach (var i in allProducts)
            {
                Console.WriteLine(i.ProductName);
            }
            return allProducts;
        }

        public Product GetProductByName()
        {
            List<Product> allProducts = new List<Product>();
            string getAll = File.ReadAllText(jsonFile);
            JsonConvert.PopulateObject(getAll, allProducts);
            foreach (var i in allProducts)
            {
                Console.WriteLine(i.ProductName);
            }
            return (Product)allProducts;
        }

        public void Insert()
        {
            List<Product> insertedProducts = new List<Product>();
            string getAll = File.ReadAllText(jsonFile);
            JsonConvert.PopulateObject(getAll, insertedProducts);

            Shop shop1 = new Shop() { ShopId = 1, ShopCity = "Borås" };

            var newProducts = new List<Product>()
            {
                new Product(){ ProductName = "Sugar", Price = 56.99,  Shop = shop1 , Manu =  new Manufacturer("ICA")},
                new Product(){ ProductName = "Lamp", Price = 86.00,  Shop = shop1 , Manu =  new Manufacturer("DEM")},
            };
            insertedProducts.AddRange(newProducts);
            foreach (var i in insertedProducts)
            {                    
                Console.WriteLine(i.ProductName + i.Price + i.Shop);
            }
            //Spara den nya listan i json igen
            using (StreamWriter savedFile = File.CreateText(@"products.json"))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(savedFile, insertedProducts);
            }
        }


        public void Delete(/*Product product*/)
        {
            List<Product> deletabledProducts = new List<Product>();
            string getAll = File.ReadAllText(@"products.json");
            JsonConvert.PopulateObject(getAll, deletabledProducts);
            //var removableProduct = from s in deletabledProducts
            //                       where s.ProductName == "Home Appliences Full Pack"
            //                       select s;
            //deletabledProducts.RemoveAll(s => deletabledProducts.Contains(s));
            //Why not this one:

                    Console.WriteLine(deletabledProducts.Capacity);
                    Console.WriteLine(deletabledProducts.Count);
                    Console.WriteLine("I am removing Home Applicences product info: ", deletabledProducts[9]);
                    //did not show product line!
                    deletabledProducts.Remove(deletabledProducts[9]);
                    deletabledProducts.ForEach(s => Console.WriteLine(s.ProductName));

            //Or:
            deletabledProducts.Remove(new Product() { ProductName = "Tea bag", Price = 1.25, Manu = new Manufacturer("Hemkop") });
            deletabledProducts.ForEach(s => Console.WriteLine(s));//NOOOO! read it from json again!
        }

        public void Save(Product productChanged)
        {
            List<Product> changedProducts = new List<Product>();
            string getAll = File.ReadAllText(@"products.json");
            JsonConvert.PopulateObject(getAll, changedProducts);
            //Byta "Towel" produkt med "Handkerchief":
            //productChanged.Name[2] = "Handkerchief";
            //Spara filen igen i json fil:
            using (StreamWriter savedFile = File.CreateText(@"products.json"))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.Serialize(savedFile, productChanged);
            }
        }

        void IProductRepository.Search()
        {
            List<Product> searchProducts = new List<Product>();
            string getAll = File.ReadAllText(@"products.json");
            JsonConvert.PopulateObject(getAll, searchProducts);
            var searchPhrase = from s in searchProducts
                               where s.ProductName == "Home Appliences Full Pack"
                               select s;
            Console.WriteLine("Please eneter your search term: ");
            var userInput = Console.ReadLine().ToLower();
            string pat = @"^.*(\bappliences\b)?.*$";

            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            Match m = r.Match(userInput);
            int matchCount = 0;
            while (m.Success)
            {
                Console.WriteLine("Match" + (++matchCount));
                for (int i = 1; i <= 2; i++)
                {
                    Group g = m.Groups[i];
                    Console.WriteLine("Group" + i + "='" + g + "'");
                    CaptureCollection cc = g.Captures;
                    for (int j = 0; j < cc.Count; j++)
                    {
                        Capture c = cc[j];
                        System.Console.WriteLine("Capture" + j + "='" + c + "', Position=" + c.Index);
                    }
                }
                m = m.NextMatch();

            }
        }

            void IProductRepository.FilterPrice()
        {
            List<Product> priceFilteredProducts = new List<Product>();
            string getAll = File.ReadAllText(@"products.json");
            JsonConvert.PopulateObject(getAll, priceFilteredProducts);

            var lessThanFifty = priceFilteredProducts.Where(s => s.Price <= 50.00).ToList();
            Console.WriteLine("Products that cost less than 50 are: ");
            foreach (var priceList in lessThanFifty)
            {
                Console.WriteLine(priceList.Price + " SEK costs one: " + priceList.ProductName);
            }

        }

        void IProductRepository.ManufacturerAndProducts()
        {
            List<Product> searchByManufact = new List<Product>();
            string getAll = File.ReadAllText(@"products.json");
            JsonConvert.PopulateObject(getAll, searchByManufact);

            var sortedAfterManufact =
                searchByManufact.GroupBy(s => s.Manu)
                    .Select(group => new 
                    {
                        ManufactName = group.Key.Name,
                        ProductCount = group.Count()
                    }
                    ).OrderBy(x => x.ManufactName);
            foreach (var group in sortedAfterManufact)
            {
                Console.WriteLine(group.ManufactName + "\t" + group.ProductCount);
            }
        }
    }
}
