using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDataRepository
    {
        //GERAL
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        //EVENTOS
        Task<Product[]> GetAllProductAsync();
        Task<Product[]> GetAllProductAsyncByName(string pProductName);
        Task<Product> GetProductAsyncById(int pProductId);

        //PALESTRANTE        
        Task<Stock[]> GetAllStockAsync(bool pIncludeProduct);
        Task<Stock> GetAllStockAsyncByProductId(int pProductId, bool pIncludeProduct = false, bool pIncludeMoveOrders = false);
        Task<Stock> GetStockAsyncById(int pStockId, bool pIncludeProduct = false, bool pIncludeMoveOrders = false);

        //PALESTRANTE        
        Task<MoveOrder[]> GetAllMoveOrderAsync(bool pIncludeProduct = false);
        Task<MoveOrder[]> GetAllMoveOrderAsyncByProductId(int pProductId, bool pIncludeProduct = false);
        Task<MoveOrder> GetMoveOrderAsyncById(int pMoveOrderId, bool pIncludeProduct = false);
    }
}
