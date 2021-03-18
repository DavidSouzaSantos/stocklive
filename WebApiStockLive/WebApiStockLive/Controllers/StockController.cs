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
    public class StockController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private readonly IMapper _mapper;

        public StockController(IDataRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #region GET
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllStocks()
        {
            try
            {
                var stocks = await _repo.GetAllStocksAsync();
                var results = _mapper.Map<StockDto[]>(stocks);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }

        [HttpGet("productName/{pProductName}")]
        [Authorize]
        public async Task<IActionResult> GetAllStocksByProductName(string pProductName)
        {
            try
            {
                var stocks = await _repo.GetAllStocksAsyncByProductName(pProductName);
                var results = _mapper.Map<StockDto[]>(stocks);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }

        [HttpGet("id/{pStockId}")]
        [Authorize]
        public async Task<IActionResult> GetStockById(int pStockId)
        {
            try
            {
                var stock = await _repo.GetStockAsyncById(pStockId);
                var results = _mapper.Map<StockDto[]>(stock);
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
        public async Task<IActionResult> AddStock(StockDto pStockDto)
        {
            try
            {
                var stock = _mapper.Map<Stock>(pStockDto);
                _repo.Add(stock);

                var stockSaved = _mapper.Map<StockDto>(stock);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/stock/id/{stock.Id}", stockSaved);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }

            return BadRequest();
        }

        [HttpPut("{pStockId}")]
        [Authorize]
        public async Task<IActionResult> UpdateStock(int pStockId, StockDto pStockDto)
        {
            try
            {
                var stock = await _repo.GetStockAsyncById(pStockId);

                if (stock == null) return NotFound();

                _mapper.Map(pStockDto, stock);
                _repo.Update(stock);

                var stockUpdated = _mapper.Map<StockDto>(stock);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/stock/id/{stock.Id}", stockUpdated);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{pStockId}")]
        [Authorize]
        public async Task<IActionResult> DeleteStock(int pStockId)
        {
            try
            {
                var stock = await _repo.GetStockAsyncById(pStockId);
                if (stock == null) return NotFound();

                _repo.Delete(stock);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok(stock);
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
