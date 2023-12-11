using Microsoft.AspNetCore.Mvc;

namespace Ejemplo1.Models.Request
{
    public class ProductRequestV2
    {
        public int ProductoID { get; set; }
        public decimal Price { get; set; }
    }
}