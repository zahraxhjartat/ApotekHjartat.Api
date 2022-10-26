using ApotekHjartat.Api.Models;
using ApotekHjartat.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Net;
using System.Threading.Tasks;
using ApotekHjartat.Common.Exceptions;
using ApotekHjartat.Api.Models.v1;

namespace ApotekHjartat.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        private CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService ?? throw new ArgumentNullException(nameof(customerOrderService));
        }
        /// <summary>
        /// Create customer order
        /// </summary>
        /// <param name="data">Customer Order Data</param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActionResult<CustomerOrderDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerOrderDto>> CreateCustomerOrder([BindRequired][FromBody] AddCustomerOrderDto data)
        {
            if (!ModelState.IsValid) return BadRequest();
            var createdCustomerOrder = await _customerOrderService.CreateCustomerOrder(data);
            return Ok(createdCustomerOrder);
        }

        /// <summary>
        /// Get customer order by id
        /// </summary>
        /// <param name="id">Customer Order Id</param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActionResult<CustomerOrderDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]

        public async Task<ActionResult<CustomerOrderDto>> GetCustomerOrderById([BindRequired] int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                return await _customerOrderService.GetCustomerOrderById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Cancel specific customer order by id
        /// </summary>
        /// <param name="id">Customer Order Id</param>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActionResult<CustomerOrderDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut("cancel/{id}")]
        public async Task<ActionResult<CustomerOrderDto>> CancelCustomerOrderById([BindRequired]int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var updatedOrder = await _customerOrderService.CancelCustomerOrderById(id);
            return Ok(updatedOrder);
        }

    }
}
