using System;
using DiscountCalculator.Domain;
using DiscountCalculator.Repository;
using DiscountCalculator.Repository.Implements;
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
    }
}
