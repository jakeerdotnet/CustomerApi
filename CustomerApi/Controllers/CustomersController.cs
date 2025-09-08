using Microsoft.AspNetCore.Mvc;
using CustomerApi.Models;
using CustomerApi.Services;

namespace CustomerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// Get all customers
    /// </summary>
    /// <returns>List of all customers</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    /// <summary>
    /// Get a customer by ID
    /// </summary>
    /// <param name="id">Customer ID</param>
    /// <returns>Customer details</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        
        if (customer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        return Ok(customer);
    }

    /// <summary>
    /// Create a new customer
    /// </summary>
    /// <param name="request">Customer details</param>
    /// <returns>Created customer</returns>
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CustomerCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            City = request.City
        };

        var createdCustomer = await _customerService.AddCustomerAsync(customer);
        
        return CreatedAtAction(
            nameof(GetCustomer), 
            new { id = createdCustomer.Id }, 
            createdCustomer);
    }

    /// <summary>
    /// Update an existing customer
    /// </summary>
    /// <param name="id">Customer ID</param>
    /// <param name="request">Updated customer details</param>
    /// <returns>Updated customer</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Customer>> UpdateCustomer(int id, [FromBody] CustomerUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            City = request.City
        };

        var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customer);
        
        if (updatedCustomer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        return Ok(updatedCustomer);
    }

    /// <summary>
    /// Delete a customer
    /// </summary>
    /// <param name="id">Customer ID</param>
    /// <returns>Success status</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var deleted = await _customerService.DeleteCustomerAsync(id);
        
        if (!deleted)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        return NoContent();
    }
}

// DTOs for requests to avoid exposing the ID field in POST/PUT requests
public class CustomerCreateRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string City { get; set; }
}

public class CustomerUpdateRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string City { get; set; }
}