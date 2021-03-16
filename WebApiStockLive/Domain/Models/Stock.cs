using System.Collections.Generic;

namespace Domain.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public List<MoveOrder> MoveOrders { get; set; }
    }
}
