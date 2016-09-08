using System;
using DiscountCalculator.Domain;
using DiscountCalculator.Repository;

namespace DiscountCalculator.ApplicationServices.Implements
{
    public class CashBackRateService : ICashBackRateService
    {
        private ICashBackRateRepository _cashBackRateRepository;

        public CashBackRateService(ICashBackRateRepository cashBackRateRepository)
        {
            _cashBackRateRepository = cashBackRateRepository;
        }

        public double GetCashBackRateByProductType(ProductType productType)
        {
            return _cashBackRateRepository.GetByProductType(productType);
        }
    }
}
