using DemoCustomerSvc.Model;

namespace DemoCustomerSvc.Repositories
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetCustomers();
        public Task<Customer> GetCustomerById(int customerId);
        public Task<Customer> AddCustomer(Customer customer);
        public Task<Customer> UpdateCustomer(Customer customer);
        public bool DeleteCustomer(int customerId);

    }
}
