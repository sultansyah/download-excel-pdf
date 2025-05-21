using DownloadExcelPDF.Data.Interfaces;
using DownloadExcelPDF.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DownloadExcelPDF.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;
        public CustomerRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection is missing");
        }
        public List<Customer> GetAllCustomer()
        {
            var list = new List<Customer>();

            using var conn = new SqlConnection(_connectionString);

            using var cmd = new SqlCommand("GetCustomerList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(
                    new Customer
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        Name = reader["Name"].ToString() ?? "-",
                        Email = reader["Email"].ToString() ?? "-",
                        JoinDate = Convert.ToDateTime(reader["JoinDate"]),
                    }
                );
            }

            return list;
        }
    }
}