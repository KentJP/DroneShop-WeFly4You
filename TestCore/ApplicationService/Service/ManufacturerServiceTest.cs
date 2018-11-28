using System;
using System.Collections.Generic;
using Droneshop.Core.ApplicationService;
using Droneshop.Core.ApplicationService.Services;
using Droneshop.Core.DomainService;
using Droneshop.Core.Entity;
using Moq;
using Xunit;

namespace TestCore
{
    public class ManufacturerServiceTest
    {
        
         
        [Fact]
        public void GetAllManufacturersEnsureRepositoryIsCalled()
        {
            var manufacturerRepo = new Mock<IManufacturerRepository>();
            IManufacturerService manufacturerService = new ManufacturerService(manufacturerRepo.Object);

            var isCalled = false;
            var filter = new Filter();


            manufacturerRepo.Setup(x => x.GetAllManufacturers(filter)).Callback(() => isCalled = true).Returns(new List<Manufacturer>()
            {
                new Manufacturer()
                {
                    Id = 1,
                    Name = "Test",
                    Drones = null                   
                }
            });

            manufacturerService.GetAllManufacturers(filter);
            
            Assert.True(isCalled);
        }

        [Fact]
        public void GetAllManufacturersWithItemsPerPageIsNegativeThrowsException()
        {
            var manufacturerRepo = new Mock<IManufacturerRepository>();
            IManufacturerService manufacturerService = new ManufacturerService(manufacturerRepo.Object);
            
            var filter = new Filter()
            {
                ItemsPerPage = -1,
                CurrentPage = 1,
            };

            var e = Assert.Throws<ArgumentException>(() => manufacturerService.GetAllManufacturers(filter));
            
            Assert.Equal("The items per page and current page have to be positive numbers", e.Message);
        }
        
        [Fact]
        public void GetAllManufacturersWithCurrentPageIsNegativeThrowsException()
        {
            var manufacturerRepo = new Mock<IManufacturerRepository>();
            IManufacturerService manufacturerService = new ManufacturerService(manufacturerRepo.Object);
            
            var filter = new Filter()
            {
                ItemsPerPage = 1,
                CurrentPage = -1,
            };

            var e = Assert.Throws<ArgumentException>(() => manufacturerService.GetAllManufacturers(filter));
            
            Assert.Equal("The items per page and current page have to be positive numbers", e.Message);
        }

        [Fact]
        public void CreateManufacturerWithoutNameThrowsException()
        {
            var manufacturerRepo = new Mock<IManufacturerRepository>();
            IManufacturerService manufacturerService = new ManufacturerService(manufacturerRepo.Object);
            
            var manufacturer = new Manufacturer()
            {
                Id = 1
            };

            var e = Assert.Throws<ArgumentException>(() => manufacturerService.Create(manufacturer));
            
            Assert.Equal("Name cannot be null or empty", e.Message);
        }

        [Fact]
        public void CreateManufacturerEnsureRepositoryIsCalled()
        {
            var manufacturerRepo = new Mock<IManufacturerRepository>();
            IManufacturerService manufacturerService = new ManufacturerService(manufacturerRepo.Object);

            var isCalled = false;
            
            var manufacturer = new Manufacturer()
            {
                Id = 1,
                Name = "Phantom"
            };


            manufacturerRepo.Setup(x => x.Create(manufacturer)).Callback(() => isCalled = true).Returns(manufacturer);

            manufacturerService.Create(manufacturer);
            
            Assert.True(isCalled);
        }

        [Fact]
        public void ReadManufacturerByIdEnsureRepositoryIsCalled()
        {
            var manufacturerRepo = new Mock<IManufacturerRepository>();
            IManufacturerService manufacturerService = new ManufacturerService(manufacturerRepo.Object);

            var isCalled = false;
            
            var manufacturer = new Manufacturer()
            {
                Id = 1,
                Name = "Phantom"
            };


            manufacturerRepo.Setup(x => x.ReadById(manufacturer.Id)).Callback(() => isCalled = true).Returns(manufacturer);

            manufacturerService.ReadById(manufacturer.Id);
            
            Assert.True(isCalled);
        }

        [Fact]
        public void ReadManufacturerByIdWithIdLowerThan1ThrowsException()
        {
            var manufacturerRepo = new Mock<IManufacturerRepository>();
            IManufacturerService manufacturerService = new ManufacturerService(manufacturerRepo.Object);
            
            var manufacturer = new Manufacturer()
            {
                Id = 0,
                Name = "Phantom"
            };

            var e = Assert.Throws<ArgumentException>(() => manufacturerService.ReadById(manufacturer.Id));
            
            Assert.Equal("The Id entered has to be at least 1", e.Message);
        }

        [Fact]
        public void ReadManufacturerByIdWithNoManufacturerFoundThrowsException()
        {
            var manufacturerRepo = new Mock<IManufacturerRepository>();
            IManufacturerService manufacturerService = new ManufacturerService(manufacturerRepo.Object);
            
            var manufacturer = new Manufacturer()
            {
                Id = 1,
                Name = "Phantom"
            };

            manufacturerRepo.Setup(x => x.ReadById(manufacturer.Id)).Returns(() => manufacturer = null);
            
            var e = Assert.Throws<ArgumentException>(() => manufacturerService.ReadById(manufacturer.Id));
            
            Assert.Equal("Could not find any manufacturer with the entered id", e.Message);
        }
    }
}