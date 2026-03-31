namespace Library1.Models
{
    public class Invoice
    {
        public string? InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Clinet client { get; set; }
        public List<InvoiceItem> InvoiceItem { get; set; }
    }
}
