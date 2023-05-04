using Azure.Core;
using BackendApi.Contracts;
using BackendApi.Contracts.User;
using BackendApi.Contracts.Orders;
using BackendApi.Contracts.Basket;
using DataAccess.Models;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class OrderssControllers : ControllerBase
    {
        private IOrderrService _orderssService;
        public OrderssControllers(IOrderrService orderssService)
        {
            _orderssService = orderssService;
        }
        /// <summary>
        /// Получение списка всех заказов БД
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderssService.GetAll());
        }



        /// <summary>
        /// Возвращение айди всех заказов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderssService.GetById(id);
            var response = new GetOrderssRequest()
            {
                OrderNumber = result.OrderNumber,
                UserIdd = result.UserIdd,
                NumberProduct = result.NumberProduct,
                DateReferences = result.DateReferences,
                Statuss = result.Statuss,
                Quantity = result.Quantity,
            };
            return Ok(await _orderssService.GetById(id));
        }

        /// <summary>
        /// Создание нового заказа
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
        /// <param name="model">Заказ</param>
        /// <returns></returns>
        // POST api/<OrderssControllers>

        [HttpPost]
        public async Task<IActionResult> Add(CreateOrderssRequest request)
        {
            var ordersDto = request.Adapt<Orderr>();
            await _orderssService.Create(ordersDto);
            return Ok();
        }
        /// <summary>
        /// Изменение сущностей записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Orderr orderss)
        {
            await _orderssService.Update(orderss);
            return Ok();
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderssService.Delete(id);
            return Ok();
        }

    }
}
//OrderNumber = result.OrderNumber
//UserIdd = result.UserIdd
//NumberProduct = result.NumberProduct
// DateReferences = result.DateReferences
// Statuss = result.Statuss
//Quantity = result.Quantity