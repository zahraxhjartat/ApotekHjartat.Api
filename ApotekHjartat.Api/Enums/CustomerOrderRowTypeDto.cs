using System.Runtime.Serialization;

namespace ApotekHjartat.Api
{
    public enum CustomerOrderRowTypeDto
    {
        [EnumMember(Value = "product")]
        Product = 0,
        [EnumMember(Value = "shipping")]
        Shipping = 1,
        [EnumMember(Value = "discount")]
        Discount = 2,
        [EnumMember(Value = "prescription")]
        Prescription = 3
    }
}
