using DiscountCalculator.Domain;

namespace DiscountCalculator.ApplicationServices
{
    public interface IDiscountCalculatorService
    {
        Amount GetAmount(double grossAmount, bool isCashier = false, ProductType productType);
    }
}
