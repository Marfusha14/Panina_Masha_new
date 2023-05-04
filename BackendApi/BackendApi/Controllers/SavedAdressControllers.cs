using Azure.Core;
using BackendApi.Contracts;
using BackendApi.Contracts.User;
using DataAccess.Models;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using BackendApi.Contracts.Basket;
using BackendApi.Contracts.Orders;
using BackendApi.Contracts.Product;
using BackendApi.Contracts.Filterr;
using BackendApi.Contracts.SavedAdress;

namespace BackendApi.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class SavedAdressControllers : ControllerBase
    {
        private ISavedAdressService _adresService;
        public SavedAdressControllers(ISavedAdressService adresService)
        {
            _adresService = adresService;
        }
        /// <summary>
        /// Получение списка всех сохраненных адресов
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _adresService.GetAll());
        }



        /// <summary>
        /// Возвращение айди всех адресов пользователей
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _adresService.GetById(id);
            var response = new GetSavedAdressRequest()
            {
                UserIdd = result.UserIdd,
                AddressId = result.AddressId,
                City = result.City,
                Street = result.Street,
                House = result.House,
                Construction = result.Construction,
                Flat = result.Flat,
                AddressName = result.AddressName,

            };
            return Ok(await _adresService.GetById(id));
        }

        /// <summary>
        /// Создание нового сохраненного адреса пользователя
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
        /// <param name="model">Сохраненные адреса</param>
        /// <returns></returns>
        // POST api/<SavedAdressControllers>

        [HttpPost]
        public async Task<IActionResult> Add(CreateSavedAdressRequest request)
        {
            var adressDto = request.Adapt<SavedAddress>();
            await _adresService.Create(adressDto);
            return Ok();
        }
        /// <summary>
        /// Изменение сущностей записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(SavedAddress adress)
        {
            await _adresService.Update(adress);
            return Ok();
        }

        /// <summary>
        /// Удаление сохраненного адреса
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _adresService.Delete(id);
            return Ok();
        }
    }
}
//UserIdd = result.UserIdd,
//AddressId = result.AddressId,
//City = result.City,
//Street = result.Street,
//House = result.House,
//Construction = result.Construction,
//Flat = result.Flat,
//AddressName = result.AddressName,
