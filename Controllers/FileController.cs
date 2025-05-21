using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Rotativa.AspNetCore;
using DownloadExcelPDF.Data.Interfaces;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DownloadExcelPDF.Controllers
{
    public class FileController : Controller
    {
        private readonly string FileName = "CustomFile";
        private readonly ICustomerRepository _repo;

        public FileController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var customers = _repo.GetAllCustomer();
            return View(customers);
        }

        // action for download pdf
        public IActionResult DownloadPDF()
        {
            var customers = _repo.GetAllCustomer();

            // tampilan pdf dengan format custom
            return new ViewAsPdf(FileName, customers)
            {
                FileName = FileName + ".pdf"
            };
        }

        // action for download excel
        public IActionResult DownloadExcel()
        {
            var customers = _repo.GetAllCustomer();

            using var workbook = new XLWorkbook();

            // add new sheet laporan
            var workSheet = workbook.Worksheets.Add("Customers");
            
            workSheet.Cell(1, 1).Value = "CustomerID";
            workSheet.Cell(1, 2).Value = "Name";
            workSheet.Cell(1, 3).Value = "Email";
            workSheet.Cell(1, 4).Value = "Join Date";

            for (int i = 0; i < customers.Count; i++)
            {
                workSheet.Cell(i + 2, 1).Value = customers[i].CustomerID;
                workSheet.Cell(i + 2, 2).Value = customers[i].Name;
                workSheet.Cell(i + 2, 3).Value = customers[i].Email;
                workSheet.Cell(i + 2, 4).Value = customers[i].JoinDate;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileName + ".xlsx"
            );
        }
    }
}