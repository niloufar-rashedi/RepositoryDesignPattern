using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Uppgift4
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductRepository productRepo = new ProductRepository();
            //productRepo.Create();
            productRepo.Delete();

            //productRepo.***Nasta Metod för att försöka**();

        }
    }
}

