using Microsoft.AspNetCore.Mvc;

namespace Ejemplo1.Models.Request
{
    public class ProductRequestV1
    {
        public string? Name { get; set; }
        public float? Price { get; set; }
    }
}