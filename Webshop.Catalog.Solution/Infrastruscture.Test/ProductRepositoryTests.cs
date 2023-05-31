using Moq;
using System;
using System.Threading.Tasks;
using Webshop.Catalog.Persistence;
using Webshop.Data.Persistence;
using Xunit;

namespace Infrastruscture.Test
{
    public class ProductRepositoryTests
    {
        private MockRepository mockRepository;

        private Mock<DataContext> mockDataContext;

        public ProductRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockDataContext = this.mockRepository.Create<DataContext>();
        }

        private ProductRepository CreateProductRepository()
        {
            return new ProductRepository(
                this.mockDataContext.Object);
        }

        [Fact]
        public async Task AddProductToCategory_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            int productId = 0;
            int categoryId = 0;

            // Act
            var result = await productRepository.AddProductToCategory(
                productId,
                categoryId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            Product entity = null;

            // Act
            await productRepository.CreateAsync(
                entity);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            int id = 0;

            // Act
            await productRepository.DeleteAsync(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();

            // Act
            var result = await productRepository.GetAll();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAllFromCategory_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            int categoryId = 0;

            // Act
            var result = await productRepository.GetAllFromCategory(
                categoryId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            int id = 0;

            // Act
            var result = await productRepository.GetById(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task RemoveProductFromCategory_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            int productId = 0;
            int categoryId = 0;

            // Act
            var result = await productRepository.RemoveProductFromCategory(
                productId,
                categoryId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var productRepository = this.CreateProductRepository();
            Product entity = null;

            // Act
            await productRepository.UpdateAsync(
                entity);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
