using Moq;
using System;
using System.Threading.Tasks;
using Webshop.Customer.Application.Contracts.Persistence;
using Webshop.Customer.Persistence;
using Webshop.Data.Persistence;
using Webshop.Domain.AggregateRoots;
using Xunit;

namespace Infrastruscture.Test
{
    public class CustomerRepositoryTests
    {
        private Mock<CustomerRepository> mockRepository;
        private Mock<Customer> mockEntity;

        public CustomerRepositoryTests()
        {
            //The code below instantiates mock objects of the respective classes
            this.mockRepository = new Mock<CustomerRepository>();
            this.mockEntity = new Mock<Customer>();

            mockEntity.Setup(name => name.Get()).Returns("Jozo Raz");
        }

        /// <summary>
        /// Returns the mock Repository
        /// </summary>
        private CustomerRepository CreateCustomerRepository()
        {
            return mockRepository.Object;
        }

        [Fact]
        public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            Customer entity = null;

            // Act
            await customerRepository.CreateAsync(
                entity);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            int id = 0;

            // Act
            await customerRepository.DeleteAsync(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();

            // Act
            var result = await customerRepository.GetAll();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            int id = 0;

            // Act
            var result = await customerRepository.GetById(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var customerRepository = this.CreateCustomerRepository();
            Customer entity = null;

            // Act
            await customerRepository.UpdateAsync(
                entity);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        public Task CreateAsync_ShouldReturnKokot(Customer entity)
        {
            using (var connection = dataContext.CreateConnection())
            {
                string command = $"insert into {TableName} (Name, Address, Address2, City, Region, PostalCode, Country, Email) values (@name, @address, @address2, @city, @region, @postalcode, @country, @email)";
                await connection.ExecuteAsync(command, new { name = entity.Name, address = entity.Address, address2 = entity.Address2, city = entity.City, region = entity.Region, postalcode = entity.PostalCode, country = entity.Country, email = entity.Email });
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
