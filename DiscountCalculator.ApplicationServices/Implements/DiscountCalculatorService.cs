using System;
using DiscountCalculator.Domain;

namespace DiscountCalculator.ApplicationServices.Implements
{
    public class DiscountCalculatorService : IDiscountCalculatorService
    {
        const double DiscountRate = 10;

        public Amount GetAmount(double grossAmount, bool isCashier, ProductType productType)
        {
            //var rate = GetDiscountRateByProductType(productType);
            // nanti semua tinggal kali rate

            Amount AmountResults = new Amount();

            var discountAmount = grossAmount * DiscountRate / 100;
            var netAmount = grossAmount - discountAmount;

            AmountResults.DiscountAmount = isCashier ? Math.Round(discountAmount, 2) : discountAmount;
            AmountResults.NetAmount = isCashier ? Math.Round(netAmount, 2) : netAmount;

            return AmountResults;
        }
    }
}
