using Domain.Models;
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
        Task<Product[]> GetAllProductsAsync();
        Task<Product[]> GetAllProductsAsyncByName(string pProductName);
        Task<Product> GetProductAsyncById(int pProductId);

        //PALESTRANTE        
        Task<Stock[]> GetAllStocksAsync(bool pIncludeProduct = false);
        Task<Stock[]> GetAllStocksAsyncByProductName(string pProductName, bool pIncludeProduct = false, bool pIncludeMoveOrders = false);
        Task<Stock> GetStockAsyncById(int pStockId, bool pIncludeProduct = false, bool pIncludeMoveOrders = false);

        //PALESTRANTE        
        Task<MoveOrder[]> GetAllMoveOrdersAsync(bool pIncludeProduct = false);
        Task<MoveOrder[]> GetAllMoveOrdersAsyncByProductId(int pProductId, bool pIncludeProduct = false);
        Task<MoveOrder> GetMoveOrderAsyncById(int pMoveOrderId, bool pIncludeProduct = false);
    }
}
