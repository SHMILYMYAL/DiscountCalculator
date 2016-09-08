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
        private List<Transaction> _transactionList;

        [TestInitialize]
        public void Setup()
        {
            var cashBackRateRepository = new Mock<ICashBackRateRepository>();           
            cashBackRateRepository.Setup(repo => repo.GetByProductType(It.Is<ProductType>(a => a == ProductType.BusinessDress))).Returns(1);
            cashBackRateRepository.Setup(repo => repo.GetByProductType(It.Is<ProductType>(a => a == ProductType.ChildrenClothes))).Returns(2);

            _cashBackService = new CashBackRateService(cashBackRateRepository.Object);
            _cashBackCalculationService = new CashBackCalculationService(cashBackRateRepository.Object);

            _transactionList = new List<Transaction>()
            {
                new Transaction { Gross = 100000, ProductType = ProductType.BusinessDress },
                new Transaction { Gross = 200000, ProductType = ProductType.BusinessDress },
                new Transaction { Gross = 50000, ProductType = ProductType.BusinessDress },
                new Transaction { Gross = 100000, ProductType = ProductType.ChildrenClothes },
                new Transaction { Gross = 200000, ProductType = ProductType.ChildrenClothes },
                new Transaction { Gross = 50000, ProductType = ProductType.ChildrenClothes }
            };
        }

        [TestMethod]
        public void GetCashBackRate_ProductType_CashBackRate()
        {
            var rate = _cashBackService.GetCashBackRateByProductType(ProductType.BusinessDress);

            Assert.AreEqual(1, rate);
        }

        [TestMethod]
        public void GetCashBackAmount_GrossAmountPerProductTypeProductType_CashBackAmount()
        {
            var gross = 350000;
            var cashBackAmount = _cashBackCalculationService.GetCashBackAmountByProductType(gross, ProductType.BusinessDress);

            Assert.AreEqual(3500, cashBackAmount);
        }

        [TestMethod]
        public void GetTotalCashBackAmount_ListTransaction_TotalCashBackAmount()
        {
            var totalCashBackAmount = _cashBackCalculationService.GetTotalCashBackAmount(_transactionList);

            Assert.AreEqual(10500, totalCashBackAmount);
        }
    }
}
