using System.ComponentModel;

namespace WebApiStockLive.Enums
{
    public enum MovementTypeEnum
    {
        [Description("Entrada")]
        In,
        [Description("Saída")]
        Out
    }
}
