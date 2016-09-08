using DiscountCalculator.Domain;
using System.Collections.Generic;

namespace DiscountCalculator.ApplicationServices
{
    public interface ICashBackCalculationService
    {
        double GetCashBackAmountByProductType(double gross, ProductType productType);

        List<CashBackTransaction> GetTotalCashBackAmount(List<Transaction> transactionList);
    }
}
