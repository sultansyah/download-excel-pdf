using DownloadExcelPDF.Models;

namespace DownloadExcelPDF.Data.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomer();
    }
}