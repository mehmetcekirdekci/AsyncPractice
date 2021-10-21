using Domain.Entities;
using Domain.Repositories;
using Moq;
using Persistence.Exceptions.CategoryExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.DomainTests.Repositories
{
    public class CategoryRepository_Test
    {
        [Fact]
        public void GetAll_Return_CategoryList()
        {
            var mock = new Mock<ICategoryRepository>();
            var categoryList = GetAllCategories();
            mock.Setup(repository => repository.GetAll()).ReturnsAsync(categoryList);
            ICategoryRepository categoryRepository = mock.Object;
            var category = categoryList[0];

            var result = categoryRepository.GetAll();

            Assert.NotNull(result);
            Assert.True(!string.IsNullOrWhiteSpace(category.Id.ToString()));
            Assert.True(!string.IsNullOrWhiteSpace(category.CategoryName));
        }

        [Fact]
        public void GetAll_ThrowException()
        {
            var mock = new Mock<ICategoryRepository>();
            List<Category> categoriesList = new List<Category>();
            mock.Setup(repository => repository.GetAll()).ReturnsAsync(() =>
            {
                if (categoriesList.Count == 0)
                {
                    throw new CategoryNotFoundException(CategoryExceptionsMessages.CategoryNotFound);
                }
                else
                {
                    return categoriesList;
                }
            });
            ICategoryRepository categoryRepository = mock.Object;

            Assert.ThrowsAsync<CategoryNotFoundException>(() => categoryRepository.GetAll());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetById_Return_Category(int id)
        {
            var mock = new Mock<ICategoryRepository>();
            var categoryList = GetAllCategories();
            mock.Setup(repository => repository.GetById(It.IsAny<int>())).ReturnsAsync(() =>
            {
                var category = categoryList.FirstOrDefault(c => c.Id == id);
                return category;
            });

            ICategoryRepository categoryRepository = mock.Object;

            var result = categoryRepository.GetById(id);

            Assert.NotNull(result);
            Assert.True(!string.IsNullOrWhiteSpace(result.Id.ToString()));
        }

        [Fact]
        public void GetById_Throw_Exception()
        {
            var mock = new Mock<ICategoryRepository>();
            var categoryList = GetAllCategories();
            mock.Setup(repository => repository.GetById(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var category = categoryList.FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    throw new CategoryNotFoundException(CategoryExceptionsMessages.CategoryNotFound);
                }
                else
                {
                    return category;
                }
            });
            ICategoryRepository categoryRepository = mock.Object;

            Assert.ThrowsAsync<CategoryNotFoundException>(() => categoryRepository.GetById(categoryList.Count + 1));
        }

        [Fact]
        public void Add()
        {
            var mock = new Mock<ICategoryRepository>();
            var categoryList = GetAllCategories();
            int categoryListCount = categoryList.Count;
            mock.Setup(repository => repository.Add(It.IsAny<Category>()));
            Category category = new Category
            {
                Id = categoryListCount,
                CategoryName = $"{categoryListCount} CategoryName"
            };
            categoryList.Add(category);
            ICategoryRepository categoryRepository = mock.Object;

            categoryRepository.Add(category);

            Assert.True(categoryListCount < categoryList.Count);
            Assert.NotNull(category);
            Assert.True(!string.IsNullOrWhiteSpace(category.Id.ToString()));
            Assert.True(!string.IsNullOrWhiteSpace(category.CategoryName));
        }

        [Fact]
        public void Add_ThrowException()
        {
            var mock = new Mock<ICategoryRepository>();
            Category category = new Category();
            mock.Setup(repository => repository.Add(It.IsAny<Category>())).Callback(() =>
            {
                if (category == null)
                {
                    throw new CategoryNotFoundException(CategoryExceptionsMessages.CategoryDontAdd);
                }
                else
                {
                    return;
                }
            });
            ICategoryRepository categoryRepository = mock.Object;

            Assert.ThrowsAsync<CategoryNotFoundException>(() => categoryRepository.Add(category));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Update(int id)
        {
            var mock = new Mock<ICategoryRepository>();
            var categoryList = GetAllCategories();
            mock.Setup(repository => repository.Update(It.IsAny<Category>()));
            Category category = new Category
            {
                Id = id,
                CategoryName = $"{id} CategoryName"
            };
            var updatedCategory = categoryList.FirstOrDefault(p => p.Id == category.Id);
            updatedCategory = category;
            ICategoryRepository categoryRepository = mock.Object;

            categoryRepository.Update(category);

            Assert.Same(category, updatedCategory);
        }

        [Fact]
        public void Update_ThrowException()
        {
            //Arrange
            var mock = new Mock<ICategoryRepository>();
            Category category = new Category();
            mock.Setup(repository => repository.Update(It.IsAny<Category>())).Callback(() =>
            {
                if (category == null)
                {
                    throw new CategoryNotFoundException(CategoryExceptionsMessages.CategoryDontUpdate);
                }
                else
                {
                    return;
                }
            });
            ICategoryRepository categoryRepository = mock.Object;

            //Assert
            Assert.ThrowsAsync<CategoryNotFoundException>(() => categoryRepository.Update(category));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Delete(int id)
        {
            var mock = new Mock<ICategoryRepository>();
            var list = GetAllCategories();
            int categoryCount = list.Count;
            mock.Setup(repository => repository.Delete(It.IsAny<int>())).Callback(() =>
            {

                var category = list.FirstOrDefault(c => c.Id == id);
                list.Remove(category);


            });

            ICategoryRepository categoryRepository = mock.Object;

            categoryRepository.Delete(id);

            Assert.True(categoryCount > list.Count);
        }

        private List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            for (int i = 0; i < 5; i++)
            {
                Category category = new Category();
                category.Id = i;
                category.CategoryName = $"{i} CategoryName";

                categories.Add(category);
            }
            return categories;
        }
    }
}
