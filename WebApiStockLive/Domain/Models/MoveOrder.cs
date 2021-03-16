using Domain.Enums;

namespace Domain.Models
{
    public class MoveOrder
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string Description { get; set; }
        public MovementTypeEnum MovementType { get; set; }
    }
}
