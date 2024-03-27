using DemoCustomerSvc.Model;
using DemoCustomerSvc.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCustomerSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        CustomerRespository customerRespository;
        public CustomerController(IConfiguration configuration)
        {
            customerRespository = new CustomerRespository(configuration);
            
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            IEnumerable<Customer> lstCustomers = null;

            if (customerRespository != null)
            {
                lstCustomers = await customerRespository.GetCustomers();
            }
            return lstCustomers;
        }

        [HttpGet("{customerID}")]
        public async Task<IActionResult> GetAllCustomerById(int customerID)
        {
            var customer = await customerRespository.GetCustomerById(customerID);
            return Ok(customer);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (customer != null)
            {
                var objCustomer = await customerRespository.AddCustomer(customer);
            }

            return Ok(customer);
        }

        [HttpPost]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            if (customer != null)
            {
                var objCustomer = await customerRespository.UpdateCustomer(customer);
            }

            return Ok(customer);
        }

        [HttpDelete("{customerID}")]
        public JsonResult Delete(int customerID)
        {
            var boolVal = customerRespository.DeleteCustomer(customerID);
            if (boolVal)
                return new JsonResult("Deleted Successfully.");
            else
                return new JsonResult("Custoemr was not deleted.");
        }

    }
}
