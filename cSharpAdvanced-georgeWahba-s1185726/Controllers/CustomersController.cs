using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;

namespace cSharpAdvanced_georgeWahba_s1185726.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer(CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllCustomers(cancellationToken);
            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerById(id, cancellationToken);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer, CancellationToken cancellationToken)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var updated = await _customerRepository.UpdateCustomer(customer, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer, CancellationToken cancellationToken)
        {
            var createdCustomer = await _customerRepository.AddCustomer(customer, cancellationToken);
            return CreatedAtAction("GetCustomer", new { id = createdCustomer.Id }, createdCustomer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
        {
            var deleted = await _customerRepository.DeleteCustomer(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
