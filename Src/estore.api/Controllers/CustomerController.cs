namespace estore.api.Controllers;

using System.Net;
using System.Text.Json;
using estore.api.Abstractions.Services;
using estore.api.Common.Pagination;
using estore.api.Common.Results;
using estore.api.Models.Requests;
using estore.api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class CustomerController(ICustomerServices customerServices) : Controller
{
    private readonly ICustomerServices _customerServices = customerServices;

    [HttpGet("{customerId}", Name = nameof(GetCustomerByCustomerId))]
    [ProducesResponseType(typeof(Result<CustomerResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetCustomerByCustomerId([FromRoute] string customerId)
    {
        var result = await _customerServices.GetCustomerByCustomerId(customerId);

        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpGet("search", Name = nameof(GetCustomerBySearchParameters))]
    [ProducesResponseType(typeof(PagedList<List<CustomerResponse>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public IActionResult GetCustomerBySearchParameters([FromQuery] SearchCustomer search)
    {
        var result = _customerServices.GetCustomers(search);

        var metadata = new
        {
            result.TotalCount,
            result.PageSize,
            result.CurrentPage,
            result.TotalPages,
            result.HasNext,
            result.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest model)
    {
        var result = await _customerServices.CreateCustomer(model);

        return result.IsSuccess ?
            CreatedAtRoute(nameof(GetCustomerByCustomerId), new { customerId = result.Value }, result) :
            BadRequest(result);
    }
}