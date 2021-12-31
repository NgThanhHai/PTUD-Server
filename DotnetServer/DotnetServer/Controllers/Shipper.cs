using DotnetServer.Models;
using DotnetServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;


namespace DotnetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class ShipperController : ControllerBase
    {
        private readonly ShipperService _shipperService;

        public ShipperController(ShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        [HttpGet]
        public ActionResult<List<Shipper>> Get() =>
            _shipperService.Get();

        [HttpGet("{id:length(24)}", Name = "GetShipper")]
        public ActionResult<Shipper> Get(string id)
        {
            var shipper = _shipperService.Get(id);

            if (shipper == null)
            {
                return NotFound();
            }

            return shipper;
        }

        [HttpPost]
        public ActionResult<Shipper> Create(Shipper shipper)
        {
            _shipperService.Create(shipper);

            return CreatedAtRoute("GetShipper", new { id = shipper._id.ToString() }, shipper);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Shipper newShipper)
        {
            var shipper = _shipperService.Get(id);

            if (shipper == null)
            {
                return NotFound();
            }

            _shipperService.Update(id, newShipper);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var shipper = _shipperService.Get(id);

            if (shipper == null)
            {
                return NotFound();
            }

            _shipperService.Remove(shipper._id);

            return NoContent();
        }
    }
}