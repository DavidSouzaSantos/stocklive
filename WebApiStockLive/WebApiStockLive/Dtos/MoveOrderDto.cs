using Domain.Enums;
using System;

namespace WebApiStockLive.Dtos
{
    public class MoveOrderDto
    {
        public ProductDto Product { get; set; }
        public string Description { get; set; }
        public DateTime DataMoviment { get; set; }
        public MovementTypeEnum MovementType { get; set; }
    }
}
