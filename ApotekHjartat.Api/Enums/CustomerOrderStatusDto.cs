
namespace ApotekHjartat.Api.Enums
{
    public enum CustomerOrderStatusDto
    {
        NotYetProccessed = 0,
        Processing = 1,
        Approved = 2,
        ReadyForPicking = 3,
        Picking = 4,
        ReadyForPacking = 5,
        Packing = 6,
        Shipped = 7,
        Cancelled = 8,
        Refunded = 9
    }
}
