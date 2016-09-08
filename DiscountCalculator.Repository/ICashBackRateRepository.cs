using DiscountCalculator.Domain;

namespace DiscountCalculator.Repository
{
    public interface ICashBackRateRepository
    {
        double GetByProductType(ProductType producType);
    }
}
