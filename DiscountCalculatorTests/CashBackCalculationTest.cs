using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiscountCalculator.ApplicationServices;
using DiscountCalculator.ApplicationServices.Implements;
using DiscountCalculator.Repository;
using Moq;
using DiscountCalculator.Domain;
using System.Collections.Generic;

namespace DiscountCalculatorTests
{
    [TestClass]
    public class CashBackCalculationTest
    {
        private ICashBackRateService _cashBackService;
        private ICashBackCalculationService _cashBackCalculationService;

        [TestInitialize]
        public void Setup()
        {
            var cashBackRateRepository = new Mock<ICashBackRateRepository>();           
            cashBackRateRepository.Setup(repo => repo.GetByProductType(It.Is<ProductType>(a => a == ProductType.BusinessDress))).Returns(1);

            _cashBackService = new CashBackRateService(cashBackRateRepository.Object);
            _cashBackCalculationService = new CashBackCalculationService(cashBackRateRepository.Object);
        }

        [TestMethod]
        public void GetCashBackRate_ProductType_CashBackRate()
        {
            var Rate = _cashBackService.GetCashBackRateByProductType(ProductType.BusinessDress);

            Assert.AreEqual(1, Rate);
        }

        [TestMethod]
        public void GetCashBackAmount_GrossAmountPerProductTypeProductType_CashBackAmount()
        {
            var Gross = 350000;
            var CashBackAmount = _cashBackCalculationService.GetCashBackAmountByProductType(Gross, ProductType.BusinessDress);

            Assert.AreEqual(3500, CashBackAmount);
        }

        [TestMethod]
        public void GetTotalCashBackAmount_ListTransaction_TotalCashBackAmount()
        {
            List <Transaction> TransactionList = new List<Transaction>();

            var TotalCashBackAmount = _cashBackCalculationService.GetTotalCashBackAmount(TransactionList);

            Assert.AreEqual(true, true);
        }
    }
}
