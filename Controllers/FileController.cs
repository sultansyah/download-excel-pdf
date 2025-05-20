using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Rotativa.AspNetCore;

namespace DownloadExcelPDF.Controllers
{
    public class FileController : Controller
    {
        private const string FileName = "CustomFile";

        // action for download pdf
        public IActionResult DownloadPDF()
        {
            var model = new
            {
                Title = "Custom PDF test",
                Date = DateTime.Now,
                Content = "Contoh Custom PDF",
            };

            // tampilan pdf dengan format custom
            return new ViewAsPdf(FileName, model)
            {
                FileName = FileName + ".pdf"
            };
        }

        // action for download excel
        public IActionResult DownloadExcel()
        {
            using var workbook = new XLWorkbook();

            // add new sheet laporan
            var workSheet = workbook.Worksheets.Add("Laporan");
            workSheet.Cell(1, 1).Value = "Tanggal";
            workSheet.Cell(1, 2).Value = "Keterangan";
            workSheet.Cell(2, 1).Value = DateTime.Now.ToString("dd/MM/yyyy");
            workSheet.Cell(2, 2).Value = "Contoh hasil excel";

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