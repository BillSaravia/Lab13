using Ejemplo1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersCustomController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public CustomersCustomController(InvoiceContext context)
        {
            _context = context;
        }

        [HttpPost]
        public List<Customer> GetInvoicesByFilter([FromBody] Customer filter)
        {

            var response = _context.Customers
                .Where(x => (x.FirstName == filter.FirstName)
                         && (x.LastName == filter.LastName))
                .OrderByDescending(x => x.LastName)
                .ToList();

            return response;
        }

        [HttpPost]
        public void Insert(RequestV1Customer request)
        {
            Customer model = new Customer();
            model.FirstName = request.FirstName;
            model.LastName = request.LastName;
            model.DocumentNumber = request.DocumentNumber;
            model.Active = true;
            _context.Customers.Add(model);
            _context.SaveChanges();// confirmacion o commit
        }

        [HttpPost]
        public void DeleteCustomer(RequestV2Customer request)
        {
            var model = _context.Customers.Find(request.CustomerID);
            _context.Entry(model).State = EntityState.Modified;
            model.Active = false;
            _context.SaveChanges();
        }

    }
}