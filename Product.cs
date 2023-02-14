using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUS20S_Project2_1_진수현_201921093
{
    internal class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int NumStock { get; set; }
        public int Price { get; set; }
        public bool isSoldOut { get; set; }

        public Product()
        {
        }
    }
}
