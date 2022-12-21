using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBI.Model.Tests
{
    [TestClass()]
    public class CashDeskTests
    {
        [TestMethod()]
        public void CashDeskTest()
        {
            //arrange 

            var customer = new Customer()
            {
                CustomerId = 1,
                Name = "testuser"
            };


            var customer2 = new Customer()
            {
                CustomerId = 2,
                Name = "testuser2"
            };
            var seller = new Seller()
            {
                SellerId = 11,
                Name = "testSeller"
            };

            var product1 = new Product()
            {
                ProductId = 1,
                Name = "pr1",
                Price = 100,
                Count = 10
            };

            var product2 = new Product()
            {
                ProductId = 2,
                Name = "pr2",
                Price = 200,
                Count = 20
            };

            var cart = new Cart(customer); //400

            var cart2 = new Cart(customer2); //700
            cart2.Add(product2);
            cart2.Add(product2);
            cart2.Add(product2);
            cart2.Add(product1);

            cart.Add(product1);
            cart.Add(product1);
            cart.Add(product2);

            var cashdesk = new CashDesk(13, seller);
            cashdesk.MaxQueueLength = 3;
            cashdesk.Enqueue(cart);
            cashdesk.Enqueue(cart2);


            var cart1ExpectedResult = 400;
            var cart2ExpectedResult = 700;
            //act
            var cart1ActualResult = cashdesk.Dequeue();
            var cart2ActualResult = cashdesk.Dequeue();

            //assert 
            Assert.AreEqual(cart1ExpectedResult, cart1ActualResult);
            Assert.AreEqual(cart2ExpectedResult, cart2ActualResult);
            Assert.AreEqual(7, product1.Count);
            Assert.AreEqual(16, product2.Count);
            //Assert.Fail();
        }

    }
}