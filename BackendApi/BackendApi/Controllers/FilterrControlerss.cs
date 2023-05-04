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

namespace BackendApi.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class FilterrControlerss : ControllerBase
    {
        private IFilterrService _filterService;
        public FilterrControlerss(IFilterrService filterService)
        {
            _filterService = filterService;
        }
        /// <summary>
        /// Получение списка всех фильтров
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _filterService.GetAll());
        }



        /// <summary>
        /// Возвращение айди всех фильтров
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _filterService.GetById(id);
            var response = new GetFilterrRequest()
            {
                IdCategories = result.IdCategories,
                ProductAvailability = result.ProductAvailability,
                ReleaseForm = result.ReleaseForm,
                AvailabilityOfDiscounts = result.AvailabilityOfDiscounts,
                Discounts = result.Discounts,
                VacationFromThePharmacy = result.VacationFromThePharmacy,
                Indications = result.Indications,
                Producer = result.Producer,
                ExpirationDate = result.ExpirationDate,
                BrandName = result.BrandName,
                QuantityInPack = result.QuantityInPack,
                Price = result.Price,
            };
            return Ok(await _filterService.GetById(id));
        }

        /// <summary>
        /// Создание нового фильтра
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
        /// <param name="model">Фильтр</param>
        /// <returns></returns>
        // POST api/<FilterrControlerss>

        [HttpPost]
        public async Task<IActionResult> Add(CreateFilterrRequest request)
        {
            var filterDto = request.Adapt<Filterr>();
            await _filterService.Create(filterDto);
            return Ok();
        }
        /// <summary>
        /// Изменение сущностей записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Filterr filter)
        {
            await _filterService.Update(filter);
            return Ok();
        }

        /// <summary>
        /// Удаление фильтра
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _filterService.Delete(id);
            return Ok();
        }

    }
}
//IdCategories = result.IdCategories,
//ProductAvailability = result.ProductAvailability,
//ReleaseForm = result.ReleaseForm,
//AvailabilityOfDiscounts = result.AvailabilityOfDiscounts,
//Discounts = result.Discounts,
//VacationFromThePharmacy = result.VacationFromThePharmacy,
//Indications = result.Indications,
//Producer = result.Producer,
//ExpirationDate = result.ExpirationDate,
//BrandName = result.BrandName,
//QuantityInPack = result.QuantityInPack,
//Price = result.Price,