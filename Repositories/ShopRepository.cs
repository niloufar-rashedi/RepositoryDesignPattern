using System;
using System.Collections.Generic;
using System.Text;

namespace Uppgift4
{
    class ShopRepository : IShopRepository
    {
        //Praktiskt använde jag inte den här klassen. Alla implementationer kommer i ProdustRepository.
        public ShopRepository()
        {}
        List<Shop> shops = new List<Shop>()
        {
        new Shop() { ShopId = 1, ShopCity = "Borås" },
        new Shop() { ShopId = 2, ShopCity = "Göteborg" },
        new Shop() { ShopId = 3, ShopCity = "Lidköping" }
        };
        public Shop GetShopById(int ShopId)
        {
            //Shop shop1 = new Shop() { ShopId = 1, ShopCity = "Borås" };
            //Shop shop2 = new Shop() { ShopId = 2, ShopCity = "Göteborg" };
            //Shop shop3 = new Shop() { ShopId = 3, ShopCity = "Lidköping" };

            return new Shop();
        }

        public void Delete(Shop shopDeleted)
        {
            throw new NotImplementedException();
        }


        public void Insert(Shop shopAdded)
        {
            throw new NotImplementedException();
        }

        public void Save(Shop shopChanged)
        {
            throw new NotImplementedException();
        }
    }
}
