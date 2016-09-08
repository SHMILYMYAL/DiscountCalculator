using DiscountCalculator.Domain;

namespace DiscountCalculator.ApplicationServices
{
    public interface IDiscountRateService
    {
        void Edit(DiscountRate model);

        DiscountRate GetDiscountRate();

        double GetDiscountRateByProductType(ProductType productType);
    }
}
