using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers(CancellationToken cancellationToken);
        Task<Customer> GetCustomerById(int id, CancellationToken cancellationToken);
        Task<Customer> AddCustomer(Customer customer, CancellationToken cancellationToken);
        Task<bool> UpdateCustomer(Customer customer, CancellationToken cancellationToken);
        Task<bool> DeleteCustomer(int id, CancellationToken cancellationToken);
    }
}
