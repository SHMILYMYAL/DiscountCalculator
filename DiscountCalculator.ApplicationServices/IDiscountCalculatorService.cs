using DiscountCalculator.Domain;

namespace DiscountCalculator.ApplicationServices
{
    public interface IDiscountCalculatorService
    {
        Amount GetAmount(double grossAmount, ProductType productType, bool isCashier = false);
    }
}
