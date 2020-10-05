using System;
using System.Collections.Generic;
using System.Text;

namespace Uppgift4
{
    public interface IShopRepository
    {
        Shop GetShopById(int ShopId);//Find specific product by ID
        void Delete(Shop shopDeleted);
        void Insert(Shop shopAdded);
        void Save(Shop shopChanged);

    }
}
