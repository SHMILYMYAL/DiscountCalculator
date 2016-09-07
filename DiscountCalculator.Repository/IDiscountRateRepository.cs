using DiscountCalculator.Domain;

namespace DiscountCalculator.Repository
{
    public interface IDiscountRateRepository
    {
        void Edit(DiscountRate model);
        DiscountRate Get();
        double GetByProductType(ProductType producType);
    }
}
