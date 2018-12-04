using System.Collections.Generic;
using Droneshop.Core.ApplicationService;
using Droneshop.Core.ApplicationService.Services;
using Droneshop.Core.DomainService;
using Droneshop.Core.Entity;
using Moq;
using Xunit;

namespace TestCore
{
    public class CustomerServiceTest
    {

        #region ReadAllCustomers

        [Fact]
        public void ReadAllCustomersEnsureRepositoryIsCalled()
        {
            var customerRepo = new Mock<ICustomerRepository>();
            ICustomerService customerService = new CustomerService(customerRepo.Object);

            var isCalled = false;

            customerRepo.Setup(x => x.ReadAllCustomers()).Callback(() => isCalled = true).Returns(new List<Customer>());

            customerService.ReadAllCustomers();
            
            Assert.True(isCalled);
        }
        
        #endregion

        #region ReadCustomerById

        [Fact]
        public void ReadCustomerByIdEnsureRepositoryIsCalled()
        {
            var customerRepo = new Mock<ICustomerRepository>();
            ICustomerService customerService = new CustomerService(customerRepo.Object);

            var isCalled = false;
            var customer = new Customer()
            {
                Id = 1,
            };

            customerRepo.Setup(x => x.ReadCustomerById(customer.Id)).Callback(() => isCalled = true).Returns(customer);

            customerService.ReadCustomerById(customer.Id);
            
            Assert.True(isCalled);
        }

        #endregion
        
        
    }
}