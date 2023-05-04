using Azure.Core;
using BackendApi.Contracts;
using BackendApi.Contracts.Basket;
using BackendApi.Contracts.User;
using BusinessLogic.Services;
using DataAccess.Models;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class BaskerController : ControllerBase
    {
        private IBasketService _bascetService;
        public BaskerController(IBasketService basketService)
        {
            _bascetService = basketService;
        }
        /// <summary>
        /// Получение списка всех корзин БД
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bascetService.GetAll());
        }



        /// <summary>
        /// Возвращение айди всех корзины
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _bascetService.GetById(id);
            var response = new GetBasketRequest()
            {
                UserIdd = result.UserIdd,
                ProductId = result.ProductId,
                QuantityOfGoods = result.QuantityOfGoods,
            };
            return Ok(await _bascetService.GetById(id));
        }

        /// <summary>
        /// Создание новой корзины
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
        /// <param name="model">Корзина</param>
        /// <returns></returns>
        // POST api/<BaskerController>

        [HttpPost]
        public async Task<IActionResult> Add(CreateBasketRequest request)
        {
            var basketto = request.Adapt<Basket>();
            await _bascetService.Create(basketto);
            return Ok();
        }
        /// <summary>
        /// Изменение сущностей записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Basket basket)
        {
            await _bascetService.Update(basket);
            return Ok();
        }

        /// <summary>
        /// Удаление корзины
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _bascetService.Delete(id);
            return Ok();
        }
    }
}
