namespace Ejemplo1.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        public float Total { get; set; }
        // Foreign Key
        public int Customer_Id { get; set; }
        public Customer Customer { get; set; }
    }
}