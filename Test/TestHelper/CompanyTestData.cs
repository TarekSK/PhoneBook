using Domain.Models;
using System;
using System.Collections.Generic;

namespace PhoneBook.UnitTest.TestHelper
{
    public static class CompanyTestData
    {
        // Company - Test Data
        public static List<Company> CompanyDefaultData()
        {
            return new List<Company>()
            {
                new Company() 
                {
                    Id = Guid.NewGuid(),
                    Name = "Company - 001",
                    RegistrationDate = DateTime.Now.AddDays(-3)
                },
                new Company() 
                {
                    Id = Guid.NewGuid(),
                    Name = "Company - 002",
                    RegistrationDate = DateTime.Now.AddDays(-10)
                },
                new Company() 
                {
                    Id = Guid.NewGuid(),
                    Name = "Company - 003",
                    RegistrationDate = DateTime.Now.AddDays(-5)
                }
            };
        }
    }
}
