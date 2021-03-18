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
    /// <summary>
    /// Move Order Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MoveOrderController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private readonly IMapper _mapper;

        public MoveOrderController(IDataRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        #region GET
        // GET api/moveorder
        /// <summary>
        /// Get all move orders
        /// </summary>
        /// <remarks>This will get all move orders</remarks>
        /// <returns>Return all move orders</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllMoveOrders()
        {
            try
            {
                var moveOrders = await _repo.GetAllMoveOrdersAsync();
                var results = _mapper.Map<MoveOrderDto[]>(moveOrders);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }

        // GET api/moveorder/productId/5
        /// <summary>
        /// Get all move orders by product id
        /// </summary>
        /// <remarks>This will get all move orders by product id</remarks>
        /// <param name="pProductId"></param>
        /// <returns>Return move orders by product id</returns>
        [HttpGet("productId/{pProductId}")]
        [Authorize]
        public async Task<IActionResult> GetAllMoveOrdersAsyncByProductId(int pProductId)
        {
            try
            {
                var moveOrders = await _repo.GetAllMoveOrdersAsyncByProductId(pProductId);
                var results = _mapper.Map<MoveOrderDto[]>(moveOrders);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }

        // GET api/moveorder/id/5
        /// <summary>
        /// Get move order by id
        /// </summary>
        /// <remarks>This will get move order by id</remarks>
        /// <param name="pMoveOrderId"></param>
        /// <returns>Return move order with set id</returns>
        [HttpGet("id/{pMoveOrderId}")]
        [Authorize]
        public async Task<IActionResult> GetMoveOrderById(int pMoveOrderId)
        {
            try
            {
                var moveOrder = await _repo.GetStockAsyncById(pMoveOrderId);
                var results = _mapper.Map<ProductDto[]>(moveOrder);
                return Ok(results);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }
        }
        #endregion

        #region Gerais
        // POST api/moveorder
        /// <summary>
        /// Add a new move order
        /// </summary>
        /// <remarks>This will add a new move order</remarks>
        /// <param name="pMoveOrderDto"></param>
        /// <returns>Return the added move order</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddStock(MoveOrderDto pMoveOrderDto)
        {
            try
            {
                var moveOrder = _mapper.Map<MoveOrder>(pMoveOrderDto);
                _repo.Add(moveOrder);

                var moveOrderSaved = _mapper.Map<MoveOrderDto>(moveOrder);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/moveorder/id/{moveOrder.Id}", moveOrderSaved);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }

            return BadRequest();
        }

        // PUT api/moveorder/1
        /// <summary>
        /// Update a move order by id
        /// </summary>
        /// <remarks>This will update move order by id</remarks>
        /// <param name="pMoveOrderId"></param>
        /// <param name="pMoveOrderDto"></param>
        /// <returns>Return the updated move order</returns>
        [HttpPut("{pMoveOrderId}")]
        [Authorize]
        public async Task<IActionResult> UpdateStock(int pMoveOrderId, MoveOrderDto pMoveOrderDto)
        {
            try
            {
                var moveOrder = await _repo.GetStockAsyncById(pMoveOrderId);

                if (moveOrder == null) return NotFound();

                _mapper.Map(pMoveOrderDto, moveOrder);
                _repo.Update(moveOrder);

                var moveOrderUpdated = _mapper.Map<MoveOrderDto>(moveOrder);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/moveorder/id/{moveOrder.Id}", moveOrderUpdated);
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou: " + e.Message);
            }

            return BadRequest();
        }

        // DELETE api/moveorder/5
        /// <summary>
        /// Delete Move Order by id
        /// </summary>
        /// <remarks>This will delete move order by id</remarks>
        /// <param name="pMoveOrderId"></param>
        /// <returns>Return move order deleted</returns>
        [HttpDelete("{pMoveOrderId}")]
        [Authorize]
        public async Task<IActionResult> DeleteStock(int pMoveOrderId)
        {
            try
            {
                var moveOrder = await _repo.GetStockAsyncById(pMoveOrderId);
                if (moveOrder == null) return NotFound();

                _repo.Delete(moveOrder);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok(moveOrder);
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
