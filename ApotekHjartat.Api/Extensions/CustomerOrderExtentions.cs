using ApotekHjartat.Api.Enums;
using ApotekHjartat.Api.Models;
using ApotekHjartat.Api.Models.v1;
using ApotekHjartat.Common.Exceptions;
using ApotekHjartat.DbAccess.Enums;
using ApotekHjartat.DbAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekHjartat.Api.Extensions
{
    public static class CustomerOrderExtentions
    {

        public static CustomerOrder ToDbModel(this AddCustomerOrderDto from)
        {
            return new CustomerOrder
            {
                CustomerAddress = from.CustomerAddress,
                CustomerEmailAddress = from.CustomerEmailAddress,
                CustomerFirstName = from.CustomerFirstName,
                CustomerSurname = from.CustomerSurname,
                CustomerOrderRows = from.CustomerOrderRows?.Select(x => x.ToDbModel()).ToList()
            };
        }

        public static CustomerOrderRow ToDbModel(this BasketRowDto from)
        {
            return new CustomerOrderRow
            {
                OrderedAmount = from.OrderedAmount,
                OrderRowType = from.BasketRowType.ToDbModel(),
                PriceExclVat = from.PriceExclVat,
                Vat = from.Vat,
                ProductId = from.ProductId,
                ProductName = from.ProductName
            };
        }


        public static CustomerOrderRowType ToDbModel(this CustomerOrderRowTypeDto value)
        {
            switch (value)
            {

                case CustomerOrderRowTypeDto.Discount:
                    return CustomerOrderRowType.Discount;
                case CustomerOrderRowTypeDto.Prescription:
                    return CustomerOrderRowType.Prescription;
                case CustomerOrderRowTypeDto.Product:
                    return CustomerOrderRowType.Product;
                case CustomerOrderRowTypeDto.Shipping:
                    return CustomerOrderRowType.Shipping;

                default:
                    throw new NotAllowedException($"Unknown CustomerOrderRowType {value}");
            }

        }

        public static CustomerOrderRowTypeDto ToDto(this CustomerOrderRowType value)
        {
            switch (value)
            {

                case CustomerOrderRowType.Discount:
                    return CustomerOrderRowTypeDto.Discount;
                case CustomerOrderRowType.Prescription:
                    return CustomerOrderRowTypeDto.Prescription;
                case CustomerOrderRowType.Product:
                    return CustomerOrderRowTypeDto.Product;
                case CustomerOrderRowType.Shipping:
                    return CustomerOrderRowTypeDto.Shipping;

                default:
                    throw new NotAllowedException($"Unknown CustomerOrderRowType {value}");
            }

        }

        public static CustomerOrderDto ToDto(this CustomerOrder from)
        {
            return new CustomerOrderDto()
            {
                CustomerOrderId = from.CustomerOrderId,
                OrderNumber = from.OrderNumber,
                Created = from.Created,
                CustomerAddress = from.CustomerAddress,
                CustomerEmailAddress = from.CustomerEmailAddress,
                CustomerFirstName = from.CustomerFirstName,
                CustomerSurname = from.CustomerSurname,
                OrderStatus = from.OrderStatus.ToDto(),
                TrackingNumber = from?.TrackingNumber,
                CustomerOrderRows = ConvertCustomerOrderRowsToDto(from.CustomerOrderRows)
            };
        }

        public static CustomerOrderDto ToClassifiedDto(this CustomerOrder from)
        {
            return new CustomerOrderDto()
            {
                CustomerOrderId = from.CustomerOrderId,
                OrderNumber = from.OrderNumber,
                Created = from.Created,
                CustomerAddress = from.CustomerAddress,
                CustomerEmailAddress = from.CustomerEmailAddress,
                CustomerFirstName = from.CustomerFirstName,
                CustomerSurname = from.CustomerSurname,
                OrderStatus = from.OrderStatus.ToDto(),
                TrackingNumber = from?.TrackingNumber,
                CustomerOrderRows = from.CustomerOrderRows?.Select(x => x.ToDto()).ToList()
            };
        }

        public static CustomerOrderStatusDto ToDto(this CustomerOrderStatus value)
        {
            switch (value)
            {

                case CustomerOrderStatus.Approved:
                    return CustomerOrderStatusDto.Approved;
                case CustomerOrderStatus.Cancelled:
                    return CustomerOrderStatusDto.Cancelled;
                case CustomerOrderStatus.NotYetProccessed:
                    return CustomerOrderStatusDto.NotYetProccessed;
                case CustomerOrderStatus.Packing:
                    return CustomerOrderStatusDto.Packing;
                case CustomerOrderStatus.Picking:
                    return CustomerOrderStatusDto.Picking;
                case CustomerOrderStatus.Processing:
                    return CustomerOrderStatusDto.Processing;
                case CustomerOrderStatus.ReadyForPacking:
                    return CustomerOrderStatusDto.ReadyForPacking;
                case CustomerOrderStatus.ReadyForPicking:
                    return CustomerOrderStatusDto.ReadyForPicking;
                case CustomerOrderStatus.Refunded:
                    return CustomerOrderStatusDto.Refunded;
                case CustomerOrderStatus.Shipped:
                    return CustomerOrderStatusDto.Shipped;
                case CustomerOrderStatus.Archived:
                    return CustomerOrderStatusDto.Archived;


                default:
                    throw new NotAllowedException($"Unknown CustomerOrderStatus {value}");
            }
        }


        public static CustomerOrderStatus ToDbModel(this CustomerOrderStatusDto value)
        {
            switch (value)
            {

                case CustomerOrderStatusDto.Approved:
                    return CustomerOrderStatus.Approved;
                case CustomerOrderStatusDto.Cancelled:
                    return CustomerOrderStatus.Cancelled;
                case CustomerOrderStatusDto.NotYetProccessed:
                    return CustomerOrderStatus.NotYetProccessed;
                case CustomerOrderStatusDto.Packing:
                    return CustomerOrderStatus.Packing;
                case CustomerOrderStatusDto.Picking:
                    return CustomerOrderStatus.Picking;
                case CustomerOrderStatusDto.Processing:
                    return CustomerOrderStatus.Processing;
                case CustomerOrderStatusDto.ReadyForPacking:
                    return CustomerOrderStatus.ReadyForPacking;
                case CustomerOrderStatusDto.ReadyForPicking:
                    return CustomerOrderStatus.ReadyForPicking;
                case CustomerOrderStatusDto.Refunded:
                    return CustomerOrderStatus.Refunded;
                case CustomerOrderStatusDto.Shipped:
                    return CustomerOrderStatus.Shipped;
                case CustomerOrderStatusDto.Archived:
                    return CustomerOrderStatus.Archived;


                default:
                    throw new NotAllowedException($"Unknown CustomerOrderStatus {value}");
            }
        }


        public static CustomerOrderRowDto ToDto(this CustomerOrderRow from)
        {
            return new CustomerOrderRowDto
            {
                OrderRowId = from.OrderRowId,
                OrderedAmount = from.OrderedAmount,
                OrderRowType = from.OrderRowType.ToDto(),
                ProductId = from.ProductId,
                ProductName = from.ProductName,
                PriceExclVat = from.PriceExclVat,
                Vat = from.Vat,
            };
        }
        private static List<CustomerOrderRowDto> ConvertCustomerOrderRowsToDto(ICollection<CustomerOrderRow> dbRows)
        {
            var dtoRows = new List<CustomerOrderRowDto>();
            var nonPrescribedRows = dbRows.Where(x => x.OrderRowType != CustomerOrderRowType.Prescription).Select(x => x.ToDto());
            var prescribedRows = dbRows.Where(x => x.OrderRowType == CustomerOrderRowType.Prescription).ToList();
            var prescriptionBagRow = new CustomerOrderRowDto { 
                ProductName = "Prescription Bag", 
                OrderRowType = CustomerOrderRowTypeDto.Prescription, 
                OrderedAmount = 1, 
                PriceExclVat = prescribedRows.Sum(x => x.PriceExclVat * x.OrderedAmount), Vat = 0M };
            dtoRows.AddRange(nonPrescribedRows);
            dtoRows.Add(prescriptionBagRow);

            return dtoRows;
        }

        public static CustomerOrderFilter ToDbModel(this CustomerOrderFilterDto from)
        {
            return new CustomerOrderFilter
            {
                CustomerOrderStatus = from.CustomerOrderStatus?.ToDbModel(),
                OrderNumber = from.OrderNumber,
                FromDate = from.FromDate,
                ToDate = from.ToDate,
                Skip = from.Skip,
                Take = from.Take
            };
        }
    }
}