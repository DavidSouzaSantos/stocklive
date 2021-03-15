using WebApiStockLive.Enums;

namespace WebApiStockLive.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public StatusEnum Status { get; set; }
    }
}
