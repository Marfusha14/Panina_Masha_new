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
    public class BasketServiceTest
    {
        private readonly BasketService service;
        private readonly Mock<IBaskerRepository> userRepositoryMoq;

        public BasketServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IBaskerRepository>();

            repositoryWrapperMoq.Setup(x => x.Basket)
                .Returns(userRepositoryMoq.Object);

            service = new BasketService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task CreateAsync_NullBasket_ShouldTrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Basket>()), Times.Never);
        }


        public static IEnumerable<object[]> GetIncorrectBasket()
        {
            return new List<object[]>
            {
                new object[] {new Basket() {  UserIdd = int.MaxValue, ProductId = int.MaxValue, QuantityOfGoods = 0 } },
                new object[] {new Basket() { UserIdd = int.MaxValue, ProductId = int.MaxValue, QuantityOfGoods = 0 } },
                new object[] {new Basket() { UserIdd = int.MaxValue, ProductId = int.MaxValue, QuantityOfGoods = 0 } },
            };
        }

        [Fact]
        public async Task CreateAsyncNewUserShouldCreateNewBasket()
        {
            var newBasket = new Basket()
            {
                UserIdd = int.MaxValue,
                ProductId = int.MaxValue,
                QuantityOfGoods = 0
            };

            await service.Create(newBasket);

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Basket>()), Times.Once);
        }


    }
}

