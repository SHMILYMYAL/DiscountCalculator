using System;
using DiscountCalculator.Domain;
using DiscountCalculator.Repository;

namespace DiscountCalculator.ApplicationServices.Implements
{
    public class DiscountRateService : IDiscountRateService
    {
        private IDiscountRateRepository _discountRateRepository;
        private IRoleRepository _roleRepository;

        public DiscountRateService(IDiscountRateRepository discountRateRepository, IRoleRepository roleRepository)
        {
            _discountRateRepository = discountRateRepository;
            _roleRepository = roleRepository;
        }

        public void Edit(DiscountRate model)
        {
            if (!_roleRepository.IsInRole(model.EditedBy, "Admin"))
                throw new ApplicationException("Not Authorize as Admin.");

            _discountRateRepository.Edit(model);
        }

        public DiscountRate GetDiscountRate()
        {
            return _discountRateRepository.Get();
        }

        public double GetDiscountRateByProductType(ProductType productType)
        {
            return _discountRateRepository.GetByProductType(productType);
        }
    }
}
