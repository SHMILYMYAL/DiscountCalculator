using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountCalculator.Domain
{
    public class CashBackTransaction
    {
        public double CashBackAmount { get; set; }

        public ProductType ProductType { get; set; }
    }
}
