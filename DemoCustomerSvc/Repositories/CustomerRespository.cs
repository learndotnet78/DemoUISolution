using DemoCustomerSvc.CoreLogic;
using DemoCustomerSvc.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DemoCustomerSvc.Repositories
{
    public class CustomerRespository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        private string? connString = null;

        public CustomerRespository(IConfiguration configuration)
        {
            _configuration = configuration;
            connString = _configuration.GetConnectionString("ConnectionString").ToString();
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("pr_Customer_GetAll", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var reader = await command.ExecuteReaderAsync();
                        customers = CoreSvc.GetList<Customer>(reader);
                    }
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return customers;
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            Customer customer = new Customer();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("pr_Customer_Get", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", customerId);
                        var reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            customer.CustomerID = (int)reader["CustomerID"];
                            customer.CustomerName = (string)reader["CustomerName"];
                            customer.Address = (string)reader["Address"];
                            customer.City = (string)reader["City"];
                            customer.State = (string)reader["State"];
                            customer.Comments = (string)reader["Comments"];
                        }
                    }
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return customer;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("pr_Customer_Post", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        command.Parameters.AddWithValue("@Address", customer.Address);
                        command.Parameters.AddWithValue("@City", customer.City);
                        command.Parameters.AddWithValue("@State", customer.State);
                        command.Parameters.AddWithValue("@Comments", customer.Comments);
                        await command.ExecuteNonQueryAsync();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("pr_Customer_Put", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        command.Parameters.AddWithValue("@Address", customer.Address);
                        command.Parameters.AddWithValue("@City", customer.City);
                        command.Parameters.AddWithValue("@State", customer.State);
                        command.Parameters.AddWithValue("@Comments", customer.Comments);
                        await command.ExecuteNonQueryAsync();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customer;
        }

        public bool DeleteCustomer(int customerId)
        {
            bool retValue = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("pr_Customer_DELETE", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", customerId);
                        command.ExecuteNonQuery();
                        retValue = true;
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retValue;
        }

    }
}
