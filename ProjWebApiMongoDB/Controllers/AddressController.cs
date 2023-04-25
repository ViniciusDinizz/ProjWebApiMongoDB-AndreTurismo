using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjWebApiMongoDB.Models;
using ProjWebApiMongoDB.Services;

namespace ProjWebApiMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet]
        public ActionResult<List<Address>> Get() => _addressService.Get();

        [HttpGet("{id:length(24)}",Name = "GetAddress")]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressService.Get(id);
            if(address == null) return NotFound();

            return address;
        }

        [HttpPost]
        public ActionResult<Address> Create(Address address)
        {
            address.City.Id = BsonObjectId.GenerateNewId().ToString();
            address.Id = BsonObjectId.GenerateNewId().ToString();
            var IdCity = _addressService.Create(address);

            if (IdCity == null) return NotFound();

            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult Update(string id, Address address)
        {
            var ckAddress = _addressService.Get(id);
            if(ckAddress == null) return NotFound();
            _addressService.Update(id, ckAddress);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            if (id == null) return NotFound();
            var ckAddress = _addressService.Get(id);
            if( ckAddress == null) return NotFound();
            _addressService.Delete(id);

            return Ok();
        }
    }
}
