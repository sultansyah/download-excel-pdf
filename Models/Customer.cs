namespace DownloadExcelPDF.Models
{
    public class Customer
    {
        public required int CustomerID { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required DateTime JoinDate { get; set; }
    }
}