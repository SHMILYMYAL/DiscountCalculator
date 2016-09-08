using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountCalculator.Domain
{
    public class Transaction
    {
        public double Gross { get; set; }

        public ProductType ProductType { get; set; }

        public int Quantity { get; set; }
    }
}
