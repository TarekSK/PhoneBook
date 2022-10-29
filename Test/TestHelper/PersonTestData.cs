using Domain.Models;
using System;
using System.Collections.Generic;

namespace PhoneBook.UnitTest.TestHelper
{
    public static class PersonTestData
    {
        // Person - Test Data - [List]
        public static List<Person> PersonDefaultData()
        {
            return new List<Person>()
            {
                new Person()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Person - 001",
                    PhoneNumber = "45287651463",
                    Address = "Birkirkara",
                    CompanyId = Guid.NewGuid(),
                },
                new Person()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Person - 002",
                    PhoneNumber = "45295421574",
                    Address = "Hamrun",
                    CompanyId = Guid.NewGuid(),
                },
                new Person()
                {
                    Id = Guid.NewGuid(),
                    FullName = "Person - 003",
                    PhoneNumber = "45236571458",
                    Address = "Attard",
                    CompanyId = Guid.NewGuid(),
                },
            };
        }

        // Person - Test Data - [Person]
        public static Person ExpectedPersonData()
        {
            return new Person() 
            { 
                Id = Guid.NewGuid(),
                FullName = "Person - " + new Random().Next(1000).ToString(),
                PhoneNumber = "35487351263",
                Address = "Msida",
                CompanyId= Guid.NewGuid(),
            };
        }
    }
}
