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
            var rate = _cashBackRateRepository.GetByProductType(productType);

            return gross * rate / 100;
        }

        public double GetTotalCashBackAmount(List<Transaction> transactionList)
        {
            double cashBackTransactionList = 0;
            var groupedTransactionListByProductType = transactionList.GroupBy(t => t.ProductType).Select(grp => grp.ToList());

            foreach (var transactionData in groupedTransactionListByProductType)
            {
                if (transactionData.Count > 1)
                {
                    cashBackTransactionList += GetCashBackAmountByProductType(transactionData.Sum(items => items.Gross), transactionData.FirstOrDefault().ProductType);
                }
            }

            return cashBackTransactionList;
        }
    }
}
