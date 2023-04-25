using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjWebApiMongoDB.Models;
using ProjWebApiMongoDB.Services;

namespace ProjWebApiMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public ActionResult<List<Customer>> Get() => _customerService.Get();

        [HttpGet("{id:length(24)}", Name="PostCustomer")]
        public ActionResult<Customer> Get(string id)
        {
            var ckCustomer = _customerService.Get(id);
            if (ckCustomer == null) return NotFound();

            return (ckCustomer);
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            if (customer.Adress.Id.Length < 24) return NotFound("Chave Address menor que 24 Caracteres.");
            var ckCustomer = _customerService.Create(customer);
            if(ckCustomer.Adress == null) return NotFound("Id de endereço não encontrado.");
            return customer;
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult Update(string id,  Customer customer)
        {
            var ckCustomer = _customerService.Get(id);
            if (ckCustomer == null) return NotFound();
            _customerService.Update(id, customer);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            var ckCustomer = _customerService.Get(id);
            if (ckCustomer == null) return NotFound();

            return Ok();
        }
    }
}
