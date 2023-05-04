using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;
using Moq;
using Xunit;

namespace BusinessLogic.Tests
{
    public class ProductServiceTest
    {
        private readonly ProductService service;
        private readonly Mock<IProductRepository> userRepositoryMoq;

        public ProductServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IProductRepository>();

            repositoryWrapperMoq.Setup(x => x.Product)
                .Returns(userRepositoryMoq.Object);

            service = new ProductService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task CreateAsync_NullProduct_ShouldTrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Product>()), Times.Never);
        }


        public static IEnumerable<object[]> GetIncorrectProduct()
        {
            return new List<object[]>
            {
                new object[] {new Product() { NumberProduct = int.MaxValue, IdCategories = int.MaxValue, Namee = "", ProductPrice =decimal.MaxValue, ProductDescription = "", Article = "" } },
                new object[] {new Product() {  NumberProduct = int.MaxValue, IdCategories = int.MaxValue, Namee = "Test", ProductPrice =decimal.MaxValue, ProductDescription = "Test", Article = "Test" } },
                new object[] {new Product() {  NumberProduct = int.MaxValue, IdCategories = int.MaxValue, Namee = "Test", ProductPrice =decimal.MaxValue, ProductDescription = "Test", Article = "Test" } },
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectProduct))]
        public async Task CreateAsyncNewUserShouldNotCreateNewUser(Product product)
        {
            var newProduct = new Product();
            //{

            //    Nickname = "",
            //    LastName = "",
            //    Namee = "",
            //    Patronymic = "",
            //    Mail = "",
            //    PhoneNumber = "",
            //    Birthdate = DateTime.MaxValue

            //};

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newProduct));

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Product>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);



        }
        [Fact]
        public async Task CreateAsyncNewUserShouldCreateNewProduct()
        {
            var newProduct = new Product()
            {
                NumberProduct = int.MaxValue,
                IdCategories = int.MaxValue,
                Namee = "Test",
                ProductPrice =decimal.MaxValue,
                ProductDescription = "Test",
                Article = "Test"
            };

            await service.Create(newProduct);

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
        }

    }
}
