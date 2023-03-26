using BusinessLayer;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// NUnit
// NUnit.Adapter
// Microsoft.NET.Test.Sdk
// Microsoft.EntityFrameworkCore.InMemory

// Test methods are executed alphabetically!

namespace TestingLayer
{
    [TestFixture]
    public class CustomerContextTest
    {
        [SetUp]
        public async Task EveryTimeSetUp()
        {
            Product p1 = new Software("Windows 7 Old School Edition", 10, 100, "7", Guid.NewGuid().ToString(), 7);
            Product p2 = new Hardware("AMD Ryzen 5 3600X", 200, 100, 700, 10);
            List<Product> products = new List<Product>() { p1, p2 };

            Customer customer = new Customer("John Doe", 33);
            customer.Products = products;
            await SetupFixture.CustomerContext.CreateAsync(customer);
        }

        [TearDown]
        public void EveryTimeSetDown()
        {
            foreach (Customer customer in SetupFixture.DbContext.Customers)
            {
                SetupFixture.DbContext.Customers.Remove(customer);
            }
        }

        [Test]
        public async Task TestCreate()
        {
            // Arrange
            Customer customer = new Customer("John Doe", 33);

            int customersBefore = SetupFixture.DbContext.Customers.Count();

            // Act
            await SetupFixture.CustomerContext.CreateAsync(customer);

            int customersAfter = SetupFixture.DbContext.Customers.Count();

            // Assert
            Assert.That(customersBefore + 1 == customersAfter, "Create does not work! ;[");
        }

        [Test]
        public async Task TestReadWithoutNavigationalProperties()
        {
            Customer firstCustomer = SetupFixture.DbContext.Customers.First();

            Customer readCustomer = await SetupFixture.CustomerContext.ReadAsync(firstCustomer.Id);

            Assert.AreEqual(firstCustomer, readCustomer, "Read does not work!");
        }

        [Test]
        public async Task TestReadWithNavigationalProperties()
        {
            Customer firstCustomer = SetupFixture.DbContext.Customers.First();

            Customer readCustomer = await SetupFixture.CustomerContext.ReadAsync(firstCustomer.Id, true);

            Assert.That(readCustomer.Products.Count != 0, "Read does not work!");
        }

        [Test]
        public async Task TestReadAll()
        {
            int customers = SetupFixture.DbContext.Customers.Count();

            int customersTest = (await SetupFixture.CustomerContext.ReadAllAsync()).Count;

            Assert.That(customers == customersTest, "Read All does not exist!");
        }

        [Test]
        public async Task TestUpdate()
        {
            Customer firstCustomer = SetupFixture.DbContext.Customers.First();

            firstCustomer.Age = 104;
            firstCustomer.Name += " Old";
            firstCustomer.Products.Add(new Software("Minecraft", 30, 100, "1.19.3", Guid.NewGuid().ToString(), 10));

            await SetupFixture.CustomerContext.UpdateAsync(firstCustomer, true);

            Customer updatedCustomer = await SetupFixture.CustomerContext.ReadAsync(firstCustomer.Id);

            Assert.AreEqual(firstCustomer, updatedCustomer, "Update method does not work!");
        }

        
        public async Task TestDelete()
        {
            int customersBefore = SetupFixture.DbContext.Customers.Count();

            Customer firstCustomer = SetupFixture.DbContext.Customers.First();

            await SetupFixture.CustomerContext.DeleteAsync(firstCustomer.Id);

            int customersAfter = SetupFixture.DbContext.Customers.Count();

            Assert.That(customersBefore == customersAfter + 1, "Delete method does not work!");

        }

    }
}
