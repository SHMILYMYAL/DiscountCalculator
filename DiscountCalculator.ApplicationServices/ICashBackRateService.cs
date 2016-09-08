using DiscountCalculator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountCalculator.ApplicationServices
{
    public interface ICashBackRateService
    {
        double GetCashBackRateByProductType(ProductType producType);
        
    }
}
