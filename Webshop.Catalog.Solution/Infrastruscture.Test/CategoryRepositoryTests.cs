using Moq;
using System;
using System.Threading.Tasks;
using Webshop.Catalog.Persistence;
using Webshop.Data.Persistence;
using Xunit;

namespace Infrastruscture.Test
{
    public class CategoryRepositoryTests
    {
        private MockRepository mockRepository;

        private Mock<DataContext> mockDataContext;

        public CategoryRepositoryTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockDataContext = this.mockRepository.Create<DataContext>();
        }

        private CategoryRepository CreateCategoryRepository()
        {
            return new CategoryRepository(
                this.mockDataContext.Object);
        }

        [Fact]
        public async Task CreateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            Category entity = null;

            // Act
            await categoryRepository.CreateAsync(
                entity);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DeleteAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            int id = 0;

            // Act
            await categoryRepository.DeleteAsync(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ExistsCategory_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            int parentId = 0;

            // Act
            var result = await categoryRepository.ExistsCategory(
                parentId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();

            // Act
            var result = await categoryRepository.GetAll();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            int id = 0;

            // Act
            var result = await categoryRepository.GetById(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetChildCategories_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            int parentCategory = 0;

            // Act
            var result = await categoryRepository.GetChildCategories(
                parentCategory);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetForProduct_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            int productId = 0;

            // Act
            var result = await categoryRepository.GetForProduct(
                productId);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetRootCategories_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();

            // Act
            var result = await categoryRepository.GetRootCategories();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var categoryRepository = this.CreateCategoryRepository();
            Category entity = null;

            // Act
            await categoryRepository.UpdateAsync(
                entity);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
