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
    public class FilterServiceTest
    {
        private readonly FilterrService service;
        private readonly Mock<IFilterrRepository> userRepositoryMoq;

        public FilterServiceTest()
        {
            var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IFilterrRepository>();

            repositoryWrapperMoq.Setup(x => x.Filterr)
                .Returns(userRepositoryMoq.Object);

            service = new FilterrService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task CreateAsync_NullFilter_ShouldTrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Filterr>()), Times.Never);
        }


        public static IEnumerable<object[]> GetIncorrectFilter()
        {
            return new List<object[]>
            {
                new object[] {new Filterr() { IdCategories = int.MaxValue, ReleaseForm = "", Discounts = int.MaxValue, VacationFromThePharmacy = "", Indications = "", Producer = "", ExpirationDate = "", BrandName = "", QuantityInPack = int.MaxValue, Price = decimal.MaxValue } },
                new object[] {new Filterr() { IdCategories = int.MaxValue, ReleaseForm = "Test", Discounts = int.MaxValue, VacationFromThePharmacy = "Test", Indications = "Test", Producer = "Test", ExpirationDate = "Test", BrandName = "Test", QuantityInPack = int.MaxValue, Price = decimal.MaxValue } },
                new object[] {new Filterr() { IdCategories = int.MaxValue, ReleaseForm = "Test", Discounts = int.MaxValue, VacationFromThePharmacy = "Test", Indications = "Test", Producer = "Test", ExpirationDate = "Test", BrandName = "Test", QuantityInPack = int.MaxValue, Price = decimal.MaxValue } },
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectFilter))]
        public async Task CreateAsyncNewUserShouldNotCreateNewUser(Filterr filter)
        {
            var newFilter = new Filterr();
            //{

            //    Nickname = "",
            //    LastName = "",
            //    Namee = "",
            //    Patronymic = "",
            //    Mail = "",
            //    PhoneNumber = "",
            //    Birthdate = DateTime.MaxValue

            //};

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newFilter));

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Filterr>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);



        }
        [Fact]
        public async Task CreateAsyncNewUserShouldCreateNewFilter()
        {
            var newFilter = new Filterr()
            {
                IdCategories = int.MaxValue,
                ReleaseForm = "Test",
                Discounts = int.MaxValue,
                VacationFromThePharmacy = "Test",
                Indications = "Test",
                Producer = "Test",
                ExpirationDate = "Test",
                BrandName = "Test",
                QuantityInPack = int.MaxValue,
                Price = decimal.MaxValue
            };

            await service.Create(newFilter);

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<Filterr>()), Times.Once);
        }



    }
}


//ReleaseForm = "Test",
//VacationFromThePharmacy = "Test",
//Indications = "Test",
//Producer = "Test",
//ExpirationDate = "Test",
//BrandName = "Test"
