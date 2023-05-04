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
    public class UserServiceTest
    {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMoq;

        public UserServiceTest()
        {
           var repositoryWrapperMoq = new Mock<IRepositoryWrapper>();
            userRepositoryMoq = new Mock<IUserRepository>();

            repositoryWrapperMoq.Setup(x => x.User) 
                .Returns(userRepositoryMoq.Object);

            service = new UserService(repositoryWrapperMoq.Object);
        }

        [Fact]
        public async Task CreateAsync_NullUser_ShouldTrowNullArgumentException()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => service.Create(null));

            Assert.IsType<ArgumentNullException>(ex);
            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
        }


        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
            {
                new object[] {new User() { Nickname= "", LastName = "", Namee = "", Patronymic = "", Mail = "", PhoneNumber = "", Birthdate = DateTime.MaxValue } },
                new object[] {new User() { Nickname= "Test", LastName = "", Namee = "", Patronymic = "", Mail = "", PhoneNumber = "", Birthdate = DateTime.MaxValue } },
                new object[] {new User() { Nickname= "Test", LastName = "Test", Namee = "", Patronymic = "", Mail = "", PhoneNumber = "", Birthdate = DateTime.MaxValue } },
            };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateAsyncNewUserShouldNotCreateNewUser(User user)
        {
            var newUser = new User();
            //{
                
            //    Nickname = "",
            //    LastName = "",
            //    Namee = "",
            //    Patronymic = "",
            //    Mail = "",
            //    PhoneNumber = "",
            //    Birthdate = DateTime.MaxValue

            //};

            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(newUser));

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
            Assert.IsType<ArgumentException>(ex);



        }
        [Fact]
        public async Task CreateAsyncNewUserShouldCreateNewUser()
        {
            var newUser = new User()
            {
                Nickname = "Test",
                LastName = "Test",
                Namee = "Test",
                Patronymic = "Test",
                Mail = "Test",
                PhoneNumber = "Test",
                Birthdate = DateTime.MaxValue
            };

            await service.Create(newUser);

            userRepositoryMoq.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }





    }
}
