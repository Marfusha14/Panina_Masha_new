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
    public class OrdersServiceTest
    {
        private readonly OrderrService service;
        private readonly Mock<IOrderrRepository> userRepositoryMoq;

        public OrdersServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IOrderrRepository>();

            repositoryWrapperMoq.Setup(x => x.Orderr)
                .Returns(userRepositoryMoq.Object);

            service = new OrderrService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task CreateAsync_NullOrders_ShouldTrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Orderr>()), Times.Never);
        }


        public static IEnumerable<object[]> GetIncorrectOrder()
        {
            return new List<object[]>
            {
                new object[] {new Orderr() {OrderNumber = int.MaxValue, UserIdd = int.MaxValue, NumberProduct = int.MaxValue, DateReferences = DateTime.MaxValue, Statuss = "", Quantity = int.MaxValue } },
                new object[] {new Orderr() { OrderNumber = int.MaxValue, UserIdd = int.MaxValue, NumberProduct = int.MaxValue, DateReferences = DateTime.MaxValue, Statuss = "Test", Quantity = int.MaxValue } },
                new object[] {new Orderr() { OrderNumber = int.MaxValue, UserIdd = int.MaxValue, NumberProduct = int.MaxValue, DateReferences = DateTime.MaxValue, Statuss = "Test", Quantity = int.MaxValue } },
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectOrder))]
        public async Task CreateAsyncNewUserShouldNotCreateNewOrder(Orderr order)
        {
            var newOrder = new Orderr();
            //{

            //    Nickname = "",
            //    LastName = "",
            //    Namee = "",
            //    Patronymic = "",
            //    Mail = "",
            //    PhoneNumber = "",
            //    Birthdate = DateTime.MaxValue

            //};

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newOrder));

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Orderr>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);



        }
        [Fact]
        public async Task CreateAsyncNewUserShouldCreateNewOrder()
        {
            var newOrder = new Orderr()
            {
                OrderNumber = int.MaxValue,
                UserIdd = int.MaxValue,
                NumberProduct = int.MaxValue,
                DateReferences = DateTime.MaxValue,
                Statuss = "Test",
                Quantity = int.MaxValue

            };

            await service.Create(newOrder);

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Orderr>()), Times.Once);
        }

    }
}
//DateReferences = DateTime.MaxValue,
//Statuss = "Test"

