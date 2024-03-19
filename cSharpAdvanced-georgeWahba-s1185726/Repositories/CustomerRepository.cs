using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using Microsoft.EntityFrameworkCore;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;

        public CustomerRepository(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers(CancellationToken cancellationToken)
        {
            return await _context.Customer.ToListAsync(cancellationToken);
        }

        public async Task<Customer> GetCustomerById(int id, CancellationToken cancellationToken)
        {
            return await _context.Customer.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<Customer> AddCustomer(Customer customer, CancellationToken cancellationToken)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return customer;
        }

        public async Task<bool> UpdateCustomer(Customer customer, CancellationToken cancellationToken)
        {
            _context.Entry(customer).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> DeleteCustomer(int id, CancellationToken cancellationToken)
        {
            var customer = await _context.Customer.FindAsync(new object[] { id }, cancellationToken);
            if (customer == null)
            {
                return false;
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
