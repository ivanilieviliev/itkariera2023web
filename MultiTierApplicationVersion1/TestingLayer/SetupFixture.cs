using DataLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace TestingLayer
{
    [SetUpFixture]
    public class SetupFixture
    {
        public static CustomerContext CustomerContext { get; private set; }

        public static OnlineShopDbContext DbContext { get; private set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // TODO: Add code here that is run before
            //  all tests in the assembly are run
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("MyTestDb");

            DbContext = new OnlineShopDbContext(builder.Options);

            CustomerContext = new CustomerContext(DbContext);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // TODO: Add code here that is run after
            //  all tests in the assembly have been run
        }
    }
}