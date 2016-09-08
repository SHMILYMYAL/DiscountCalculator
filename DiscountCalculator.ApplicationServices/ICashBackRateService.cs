using DiscountCalculator.Domain;

namespace DiscountCalculator.ApplicationServices
{
    public interface ICashBackRateService
    {
        double GetCashBackRateByProductType(ProductType producType);
        
    }
}
