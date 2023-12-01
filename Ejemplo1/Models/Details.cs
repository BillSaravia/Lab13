namespace Ejemplo1.Models
{
    public class Details
    {
        public int DetailsId { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float SubTotal { get; set; }

        // Foreign Key - Product
        public int Product_Id { get; set; }
        public Product Product { get; set; }

        // Foreign Key - Invoice
        public int Invoice_Id { get; set; }
        public Invoice Invoice { get; set; }

    }
}