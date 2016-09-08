using System;
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

        public List<CashBackTransaction> GetTotalCashBackAmount(List<Transaction> transactionList)
        {
            List<CashBackTransaction> CashBackTransactionList = new List<CashBackTransaction>();
            var GroupedTransactionListByProductType = transactionList.GroupBy(t => t.ProductType).Select(grp => grp.ToList());

            foreach (var transactionData in GroupedTransactionListByProductType)
            {
                if (transactionData.Count > 1)
                {
                    CashBackTransactionList.Add(
                        new CashBackTransaction
                        {
                            CashBackAmount = GetCashBackAmountByProductType(transactionData.Sum(items => items.Gross), transactionData.FirstOrDefault().ProductType),
                            ProductType = transactionData.FirstOrDefault().ProductType
                        });
                }
            }

            return CashBackTransactionList;
        }
    }
}
