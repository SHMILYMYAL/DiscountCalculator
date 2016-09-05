using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiscountCalculator.ApplicationServices;
using DiscountCalculator.ApplicationServices.Implements;
using DiscountCalculator.Repository;
using Moq;
using DiscountCalculator.Domain;

namespace DiscountCalculatorTests
{
    [TestClass]
    public class DiscountCalculationTest
    {
        private IDiscountCalculatorService _discountCalculatorService;
        private IDiscountRateService _discountService;

        [TestInitialize]
        public void Setup()
        {
            var discountRateRepository = new Mock<IDiscountRateRepository>();
            discountRateRepository.Setup(repo => repo.Get()).Returns(new DiscountRate {
                Amount = 0.05,
                EditedBy = 1
            });

            var roleRepository = new Mock<IRoleRepository>();
            roleRepository.Setup(repo => repo.IsInRole(1, "Admin")).Returns(true);
            roleRepository.Setup(repo => repo.IsInRole(2, "Admin")).Returns(false);

            _discountCalculatorService = new DiscountCalculatorService();
            _discountService = new DiscountRateService(discountRateRepository.Object, roleRepository.Object);
        }

        [TestMethod]
        public void GetNetAmount_GrossAmountWithDefaultDiscountRateAndIsCashier_NetAmount()
        {
            var gross = 100000;
            var netAmount = _discountCalculatorService.GetNetAmount(gross, true);

            Assert.AreEqual(90000, netAmount);
        }

        [TestMethod]
        public void GetNetAmount_GrossAmountWithDefaultDiscountRateAndIsNotCashier_NetAmountNotRoundedByTwo()
        {
            var gross = 100000;
            var netAmount = _discountCalculatorService.GetNetAmount(gross);

            Assert.AreEqual(90000, netAmount);
        }

        [TestMethod]
        public void Edit_DiscountRateAsAdmin_DiscountRateUpdated()
        {
            var discountRate = _discountService.GetDiscountRate();
            discountRate.Amount = 0.05;
            discountRate.EditedBy = 1; // Administrator

            _discountService.Edit(discountRate);

            Assert.AreEqual(0.05, _discountService.GetDiscountRate().Amount);
        }

        [TestMethod]
        public void Edit_DiscountRateNotAsAdmin_DiscountRateUpdated()
        {
            var discountRate = _discountService.GetDiscountRate();
            discountRate.Amount = 0.05;
            discountRate.EditedBy = 2; // NotAdministrator

            try
            {
                _discountService.Edit(discountRate);
            }
            catch (ApplicationException appEx)
            {
                Assert.AreEqual(appEx.Message, "Not Authorize as Admin.");
            }
        }
    }
}
