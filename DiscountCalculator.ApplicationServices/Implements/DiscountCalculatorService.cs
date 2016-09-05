using System;

namespace DiscountCalculator.ApplicationServices.Implements
{
    public class DiscountCalculatorService : IDiscountCalculatorService
    {
        const double DiscountRate = 0.1;

        public double GetNetAmount(double grossAmount, bool isCashier)
        {
            var netAmount = grossAmount -(grossAmount * DiscountRate);
            return isCashier ? Math.Round(netAmount, 2) : netAmount;
        }
    }
}
