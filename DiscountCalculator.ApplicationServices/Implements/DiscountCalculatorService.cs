using System;
using DiscountCalculator.Domain;
using DiscountCalculator.Repository;

namespace DiscountCalculator.ApplicationServices.Implements
{
    public class DiscountCalculatorService : IDiscountCalculatorService
    {
        private IDiscountRateRepository _discountRateRepository;

        public DiscountCalculatorService(IDiscountRateRepository discountRateRepository)
        {
            _discountRateRepository = discountRateRepository;
        }

        public Amount GetAmount(double grossAmount, ProductType productType, bool isCashier)
        {
            var rate = _discountRateRepository.GetByProductType(productType);

            Amount amountResults = new Amount();

            var discountAmount = grossAmount * rate / 100;
            var netAmount = grossAmount - discountAmount;

            amountResults.DiscountAmount = isCashier ? Math.Round(discountAmount, 2) : discountAmount;
            amountResults.NetAmount = isCashier ? Math.Round(netAmount, 2) : netAmount;

            return amountResults;
        }
    }
}
