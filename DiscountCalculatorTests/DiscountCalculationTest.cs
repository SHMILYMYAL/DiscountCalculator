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
                Rate = 10,
                EditedBy = 1,
                ProductType = ProductType.Default
            });

            var roleRepository = new Mock<IRoleRepository>();
            roleRepository.Setup(repo => repo.IsInRole(1, "Admin")).Returns(true);
            roleRepository.Setup(repo => repo.IsInRole(2, "Admin")).Returns(false);

            _discountCalculatorService = new DiscountCalculatorService();
            _discountService = new DiscountRateService(discountRateRepository.Object, roleRepository.Object);
        }

        [TestMethod]
        public void GetAmount_GrossAmountWithDefaultDiscountRateAndIsCashier_Amount()
        {
            var gross = 100000;
            var Amount = _discountCalculatorService.GetAmount(gross, true, ProductType.Default);

            Assert.AreEqual(90000, Amount.NetAmount);
            Assert.AreEqual(10000, Amount.DiscountAmount);
        }

        [TestMethod]
        public void Edit_DiscountRateAsAdmin_DiscountRateUpdated()
        {
            var discountRate = _discountService.GetDiscountRate();
            discountRate.Rate = 5;
            discountRate.EditedBy = 1; // Administrator

            _discountService.Edit(discountRate);

            Assert.AreEqual(5, _discountService.GetDiscountRate().Rate);
        }

        [TestMethod]
        public void Edit_DiscountRateNotAsAdmin_DiscountRateNotUpdated()
        {
            var discountRate = _discountService.GetDiscountRate();
            discountRate.Rate = 5;
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

        [TestMethod]
        public void GetAmount_GrossAmountWithDefaultDiscountRateAndIsNotCashier_AmountNotRoundedByTwo()
        {
            var gross = 5.89;
            var Amount = _discountCalculatorService.GetAmount(gross, false, ProductType.Default);

            Assert.AreEqual(5.301, Amount.NetAmount);
            Assert.AreEqual(0.589, Amount.DiscountAmount);
        }

        [TestMethod]
        public void SetDiscountRate_DiscountRateAndProductTypeAsAdmin_DiscountRateUpdated()
        {
            var discountRate = _discountService.GetDiscountRate();
            discountRate.Rate = 5;
            discountRate.EditedBy = 1; // Administrator
            discountRate.ProductType = ProductType.BusinessDress; // Business Dress

            _discountService.Edit(discountRate);

            Assert.AreEqual(5, _discountService.GetDiscountRate().Rate);
        }

        [TestMethod]
        public void GetAmount_GrossAmountProductTypeAndIsCashier_Amount()
        {
            var gross = 100000;
            var Amount = _discountCalculatorService.GetAmount(gross, true, ProductType.BusinessDress);

            Assert.AreEqual(95000, Amount.NetAmount);
            Assert.AreEqual(5000, Amount.DiscountAmount);
        }
    }
}
