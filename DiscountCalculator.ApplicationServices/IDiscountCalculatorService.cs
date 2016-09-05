namespace DiscountCalculator.ApplicationServices
{
    public interface IDiscountCalculatorService
    {
        double GetNetAmount(double grossAmount, bool isCashier = false);
    }
}
