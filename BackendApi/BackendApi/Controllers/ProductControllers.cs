using Azure.Core;
using BackendApi.Contracts;
using BackendApi.Contracts.User;
using BackendApi.Contracts.Basket;
using BackendApi.Contracts.Orders;
using BackendApi.Contracts.Product;
using DataAccess.Models;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class ProductControllers : ControllerBase
    {
        private IProductService _productService;
        public ProductControllers(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// Получение списка всех товаров
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }



        /// <summary>
        /// Возвращение айди всех товаров
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetById(id);
            var response = new GetProductRequest()
            {
                NumberProduct = result.NumberProduct,
                IdCategories = result.IdCategories,
                Namee = result.Namee,
                ProductPrice = result.ProductPrice,
                ProductDescription = result.ProductDescription,
                Article = result.Article,

            };
            return Ok(await _productService.GetById(id));
        }

        /// <summary>
        /// Создание нового товара
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
        /// <param name="model">Товар</param>
        /// <returns></returns>
        // POST api/<ProductControllers>

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductRequest request)
        {
            var productDto = request.Adapt<Product>();
            await _productService.Create(productDto);
            return Ok();
        }
        /// <summary>
        /// Изменение сущностей записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            await _productService.Update(product);
            return Ok();
        }

        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return Ok();
        }
    }
}
//NumberProduct = result.NumberProduct
//IdCategories = result.IdCategories
// Namee = result.Namee
//ProductPrice = result.ProductPrice
//ProductDescription = result.ProductDescription
//Article = result.Article
