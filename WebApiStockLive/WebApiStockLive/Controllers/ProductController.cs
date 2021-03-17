using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Threading.Tasks;
using WebApiStockLive.Dtos;

namespace WebApiStockLive.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private readonly IMapper _mapper;

        public ProductController(IDataRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #region GET
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _repo.GetAllProductsAsync();
                var results = _mapper.Map<ProductDto[]>(products);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }

        [HttpGet("name/{pProductName}")]
        [Authorize]
        public async Task<IActionResult> GetAllProductsByName(string pProductName)
        {
            try
            {
                var products = await _repo.GetAllProductsAsyncByName(pProductName);
                var results = _mapper.Map<ProductDto[]>(products);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }

        [HttpGet("id/{pProductId}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(int pProductId)
        {
            try
            {
                var products = await _repo.GetProductAsyncById(pProductId);
                var results = _mapper.Map<ProductDto[]>(products);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }
        #endregion

        #region Gerais
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProduct(ProductDto pProductDto)
        {
            try
            {
                var product = _mapper.Map<Product>(pProductDto);
                _repo.Add(product);

                var productSaved = _mapper.Map<ProductDto>(product);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/product/id/{product.Id}", productSaved);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }

            return BadRequest();
        }

        [HttpPut("{pProductId}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int pProductId, ProductDto pProductDto)
        {
            try
            {
                var product = await _repo.GetProductAsyncById(pProductId);

                if (product == null) return NotFound();

                _mapper.Map(pProductDto, product);
                _repo.Update(product);

                var productUpdated = _mapper.Map<ProductDto>(product);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/product/id/{product.Id}", productUpdated);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{pProductId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int pProductId)
        {
            try
            {
                var product = await _repo.GetProductAsyncById(pProductId);
                if (product == null) return NotFound();

                _repo.Delete(product);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok(product);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }

            return BadRequest();
        }
        #endregion


    }
}
