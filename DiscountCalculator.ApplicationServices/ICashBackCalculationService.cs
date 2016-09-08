using DiscountCalculator.Domain;
using System.Collections.Generic;

namespace DiscountCalculator.ApplicationServices
{
    public interface ICashBackCalculationService
    {
        double GetCashBackAmountByProductType(double gross, ProductType productType);

        double GetTotalCashBackAmount(List<Transaction> transactionList);
    }
}
