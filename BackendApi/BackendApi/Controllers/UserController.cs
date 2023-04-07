using Azure.Core;
using BackendApi.Contracts;
using BackendApi.Contracts.User;
using DataAccess.Models;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
namespace BackendApi.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Получение списка всех пользователей БД
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }



        /// <summary>
        /// Возвращение айди всех пользователей пользователей
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);
            var response = new GetUserResponse()
            {
                UserNumber = result.UserNumber,
                Nickname = result.Nickname,
                LastName = result.LastName,
                Namee = result.Namee,
                Patronymic = result.Patronymic,
                Mail = result.Mail,
                PhoneNumber = result.PhoneNumber,
                Birthdate = result.Birthdate,
            };
            return Ok(await _userService.GetById(id));
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "login" : "A4Tech Bloody B188",
        ///         "password" : "!Pa$$word123@",
        ///         "firstname" : "Иван",
        ///         "lastname" : "Иванов",
        ///         "middlename" : "Иванович"
        ///     }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        // POST api/<UsersController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            var userDto = request.Adapt<User>();
            await _userService.Create(userDto);
            return Ok();
        }
        /// <summary>
        /// Изменение сущностей записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await _userService.Update(user);
            return Ok();
        }

       /// <summary>
       /// Удаление пользователей
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }

    }
}
