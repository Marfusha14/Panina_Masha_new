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
    public class savedAdressServiceTest
    {
        private readonly SavedAdressService service;
        private readonly Mock<ISavedadressRepository> userRepositoryMoq;

        public savedAdressServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<ISavedadressRepository>();

            repositoryWrapperMoq.Setup(x => x.SevedAdress)
                .Returns(userRepositoryMoq.Object);

            service = new SavedAdressService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task CreateAsync_NullAdress_ShouldTrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<SavedAddress>()), Times.Never);
        }


        public static IEnumerable<object[]> GetIncorrectAdress()
        {
            return new List<object[]>
            {
                new object[] {new SavedAddress() { UserIdd = int.MaxValue, AddressId = int.MaxValue, City = "", Street = "", House = int.MaxValue, Construction = int.MaxValue, Flat = int.MaxValue, AddressName = "" } },
                new object[] {new SavedAddress() { UserIdd = int.MaxValue, AddressId = int.MaxValue, City = "Test", Street = "Test", House = int.MaxValue, Construction = int.MaxValue, Flat = int.MaxValue, AddressName = "Test" } },
                new object[] {new SavedAddress() { UserIdd = int.MaxValue, AddressId = int.MaxValue, City = "Test", Street = "Test", House = int.MaxValue, Construction = int.MaxValue, Flat = int.MaxValue, AddressName = "Test" } },
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectAdress))]
        public async Task CreateAsyncNewUserShouldNotCreateNewUser(SavedAddress adres)
        {
            var newAdress = new SavedAddress();
            //{

            //    Nickname = "",
            //    LastName = "",
            //    Namee = "",
            //    Patronymic = "",
            //    Mail = "",
            //    PhoneNumber = "",
            //    Birthdate = DateTime.MaxValue

            //};

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newAdress));

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<SavedAddress>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);



        }
        [Fact]
        public async Task CreateAsyncNewUserShouldCreateNewAdress()
        {
            var newAdress = new SavedAddress()
            {
                UserIdd = int.MaxValue,
                AddressId = int.MaxValue,
                City = "Test",
                Street = "Test",
                House = int.MaxValue,
                Construction = int.MaxValue,
                Flat = int.MaxValue,
                AddressName = "Test"

            };

            await service.Create(newAdress);

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<SavedAddress>()), Times.Once);
        }

    }
}
