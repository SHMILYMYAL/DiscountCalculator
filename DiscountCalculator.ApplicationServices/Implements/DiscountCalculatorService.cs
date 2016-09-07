using System;
using DiscountCalculator.Domain;
using DiscountCalculator.Repository;

namespace DiscountCalculator.ApplicationServices.Implements
{
    public class DiscountCalculatorService : IDiscountCalculatorService
    {
        //const double DiscountRate = 10;
        private IDiscountRateRepository _discountRateRepository;

        public DiscountCalculatorService(IDiscountRateRepository discountRateRepository)
        {
            _discountRateRepository = discountRateRepository;
        }

        public Amount GetAmount(double grossAmount, ProductType productType, bool isCashier)
        {
            var Rate = _discountRateRepository.GetByProductType(productType);

            Amount AmountResults = new Amount();

            var discountAmount = grossAmount * Rate / 100;
            var netAmount = grossAmount - discountAmount;

            AmountResults.DiscountAmount = isCashier ? Math.Round(discountAmount, 2) : discountAmount;
            AmountResults.NetAmount = isCashier ? Math.Round(netAmount, 2) : netAmount;

            return AmountResults;
        }
    }
}
