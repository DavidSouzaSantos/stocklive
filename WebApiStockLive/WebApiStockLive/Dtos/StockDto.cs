using System.Collections.Generic;

namespace WebApiStockLive.Dtos
{
    public class StockDto
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public List<MoveOrderDto> MoveOrders { get; set; }
    }
}
