using Domain.Enums;

namespace WebApiStockLive.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public StatusEnum Status { get; set; }
    }
}
