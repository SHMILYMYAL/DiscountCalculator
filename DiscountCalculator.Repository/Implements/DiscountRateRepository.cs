﻿using DiscountCalculator.Domain;
using System;

namespace DiscountCalculator.Repository.Implements
{
    public class DiscountRateRepository : IDiscountRateRepository
    {
        //Entity Framework

        public void Edit(DiscountRate model)
        {
            // Edit to tabel Discount Rate Database
            throw new NotImplementedException();
        }

        public DiscountRate Get()
        {
            // Get Query Discount Rate 
            throw new NotImplementedException();
        }

        public double GetByProductType(ProductType productType)
        {
            // Get Query Discount Rate By Product Type
            throw new NotImplementedException();
        }
    }
}
