using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;
        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<User>> GetAll()
        {
            return await _repositoryWrapper.User.FindAll();
        }
        public async Task<User> GetById(int id)
        {
            var user = await _repositoryWrapper.User
            .FindByCondition(x => x.UserNumber == id);
            return user.First();
        }
        public async Task Create(User model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }


            if (string.IsNullOrEmpty(model.Nickname))
            {
                throw new ArgumentException(nameof(model.Nickname));
            }
            await _repositoryWrapper.User.Create(model);
            await _repositoryWrapper.Save();
        }
        public async Task Update(User model)
        {
            await _repositoryWrapper.User.Update(model);
            await _repositoryWrapper.Save();
        }
        public async Task Delete(int id)
        {
            var user = await _repositoryWrapper.User
            .FindByCondition(x => x.UserNumber == id);
            await _repositoryWrapper.User.Delete(user.First());
            await _repositoryWrapper.Save();
        }
        public async Task<User> Login(string email, string password)
        {
            var user = await _repositoryWrapper.User.GetByEmailAndPassword(email, password);
            return user;
        }
    }
}

//@using BlazorServer.Auth;
//@inherits LayoutComponentBase
//@inject AuthenticationStateProvider authStateProvide
//@inject NavigationManager navManager

//<PageTitle>Web</PageTitle>

//<div class="page">
//    <div class="sidebar">
//        <NavMenu />
//    </div>

//    <main>
//        <div class="top-row px-4">
//            <a href = "hhtps://docs.microsoft.com/aspnet/"
//        </ div >
//    </ main >



//</ div >

