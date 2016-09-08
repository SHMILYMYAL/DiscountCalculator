using System.Collections.Generic;
using DiscountCalculator.Domain;
using DiscountCalculator.Repository;
using System.Linq;

namespace DiscountCalculator.ApplicationServices.Implements
{
    public class CashBackCalculationService : ICashBackCalculationService
    {
        private ICashBackRateRepository _cashBackRateRepository;

        public CashBackCalculationService(ICashBackRateRepository cashBackRateRepository)
        {
            _cashBackRateRepository = cashBackRateRepository;
        }

        public double GetCashBackAmountByProductType(double gross, ProductType productType)
        {
            var Rate = _cashBackRateRepository.GetByProductType(productType);

            return gross * Rate / 100;
        }

        public double GetTotalCashBackAmount(List<Transaction> transactionList)
        {
            double CashBackTransactionList = 0;
            var GroupedTransactionListByProductType = transactionList.GroupBy(t => t.ProductType).Select(grp => grp.ToList());

            foreach (var transactionData in GroupedTransactionListByProductType)
            {
                if (transactionData.Count > 1)
                {
                    CashBackTransactionList += GetCashBackAmountByProductType(transactionData.Sum(items => items.Gross), transactionData.FirstOrDefault().ProductType);
                }
            }

            return CashBackTransactionList;
        }
    }
}
