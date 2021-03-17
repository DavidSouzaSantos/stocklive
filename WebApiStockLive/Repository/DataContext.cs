using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region Gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        #endregion

        #region Product
        public async Task<Product[]> GetAllProductsAsync()
        {
            IQueryable<Product> query = _context.Products;

            query = query.OrderByDescending(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Product[]> GetAllProductsAsyncByName(string pProductName)
        {
            IQueryable<Product> query = _context.Products;

            query = query.Where(p => p.Name.ToLower().Contains(pProductName)).OrderByDescending(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Product> GetProductAsyncById(int pProductId)
        {
            IQueryable<Product> query = _context.Products;

            query = query.Where(p => p.Id == pProductId).OrderByDescending(p => p.Id);

            return await query.FirstOrDefaultAsync();
        }
        #endregion

        #region Stock
        public async Task<Stock[]> GetAllStocksAsync(bool pIncludeProduct = false)
        {
            IQueryable<Stock> query = _context.Stocks;

            if (pIncludeProduct)
            {
                query = query
                    .Include(p => p.Product);
            }

            query = query.OrderByDescending(s => s.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Stock> GetAllStocksAsyncByProductId(int pProductId, bool pIncludeProduct = false, bool pIncludeMoveOrders = false)
        {
            IQueryable<Stock> query = _context.Stocks;

            if (pIncludeProduct)
            {
                query = query
                    .Include(p => p.Product);
            }

            if (pIncludeMoveOrders)
            {
                query = query
                    .Include(p => p.MoveOrders);
            }

            query = query.Where(s => s.Product.Id == pProductId).OrderByDescending(s => s.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Stock> GetStockAsyncById(int pStockId, bool pIncludeProduct = false, bool pIncludeMoveOrders = false)
        {
            IQueryable<Stock> query = _context.Stocks;

            if (pIncludeProduct)
            {
                query = query
                    .Include(p => p.Product);
            }

            if (pIncludeMoveOrders)
            {
                query = query
                    .Include(p => p.MoveOrders);
            }

            query = query.Where(s => s.Id == pStockId).OrderByDescending(s => s.Id);

            return await query.FirstOrDefaultAsync();
        }
        #endregion

        #region MoveOrder
        public async Task<MoveOrder[]> GetAllMoveOrdersAsync(bool pIncludeProduct = false)
        {
            IQueryable<MoveOrder> query = _context.MoveOrders;

            if (pIncludeProduct)
            {
                query = query
                    .Include(p => p.Product);
            }

            query = query.OrderByDescending(m => m.Id);

            return await query.ToArrayAsync();
        }

        public async Task<MoveOrder[]> GetAllMoveOrdersAsyncByProductId(int pProductId, bool pIncludeProduct = false)
        {
            IQueryable<MoveOrder> query = _context.MoveOrders;

            if (pIncludeProduct)
            {
                query = query
                    .Include(p => p.Product);
            }

            query = query.Where(m => m.Product.Id == pProductId).OrderByDescending(m => m.Id);

            return await query.ToArrayAsync();
        }

        public async Task<MoveOrder> GetMoveOrderAsyncById(int pMoveOrderId, bool pIncludeProduct = false)
        {
            IQueryable<MoveOrder> query = _context.MoveOrders;

            if (pIncludeProduct)
            {
                query = query
                    .Include(p => p.Product);
            }

            query = query.Where(m => m.Id == pMoveOrderId).OrderByDescending(m => m.Id);

            return await query.FirstOrDefaultAsync();
        }
        #endregion

    }
}
