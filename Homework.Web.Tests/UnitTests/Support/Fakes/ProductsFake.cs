using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Web.Tests.UnitTests.Support.Fake
{
    public class ProductsFake
    {
        public List<ProductFake> Products { get; set; } = new List<ProductFake>();
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
    }
}
