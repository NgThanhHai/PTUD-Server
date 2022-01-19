using DotnetServer.Models;
using DotnetServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotnetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _shhopService;

        public ShopController(ShopService shopService)
        {
            _shhopService = shopService;
        }

        [HttpGet]
        public ActionResult<List<Shop>> Get() =>
            _shhopService.Get();

        [HttpGet("{id:length(24)}", Name = "GetShop")]
        public ActionResult<Shop> Get(string id)
        {
            var shipper = _shhopService.Get(id);

            if (shipper == null)
            {
                return NotFound();
            }

            return shipper;
        }

        [HttpPost]
        public ActionResult<Shop> Create(Shop shipper)
        {
            _shhopService.Create(shipper);

            return CreatedAtRoute("GetShop", new { id = shipper._id.ToString() }, shipper);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Shop newShop)
        {
            var shipper = _shhopService.Get(id);

            if (shipper == null)
            {
                return NotFound();
            }

            _shhopService.Update(id, newShop);

            return CreatedAtRoute("GetShop", new { id = newShop._id.ToString() }, newShop);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var shipper = _shhopService.Get(id);

            if (shipper == null)
            {
                return NotFound();
            }

            _shhopService.Remove(shipper._id);

            return NoContent();
        }
    }
}