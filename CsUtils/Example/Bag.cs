using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsUtils;

namespace Example
{
    internal class Item
    {
        public int Id = 200;
        public string Name = "物品";
    }

    internal class Bag
    {
        public int Id = 100;
        public string Name = "背包";
    }

    internal class BagManager : Singleton<BagManager>
    {
        public int Id = 100;
        public string Name = "背包管理器";
    }
}
