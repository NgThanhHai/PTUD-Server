using DotnetServer.Models;
using DotnetServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _shopService;
        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }

        [ActionName("GetAll")]
        [HttpGet]
        public ActionResult<List<Shop>> Get() =>
            _shopService.Get();

        [ActionName("Get")]
        [HttpGet("{id:length(24)}", Name = "GetShop")]
        public ActionResult<Shop> Get(string id)
        {
            var shop = _shopService.Get(id);

            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        [ActionName("Update")]
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Shop shopUpdate)
        {
            var shop = _shopService.Get(id);

            if (shop == null)
            {
                return NotFound();
            }

            _shopService.Update(id, shopUpdate);

            return CreatedAtRoute("GetShop", new { id = shopUpdate._id.ToString() }, shopUpdate);
        }

        [ActionName("Delete")]
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var shop = _shopService.Get(id);

            if (shop == null)
            {
                return NotFound();
            }

            _shopService.Remove(shop._id);

            return NoContent();
        }
        [HttpPost]
        public ActionResult<Shop> Create(Shop shop)
        {
            _shopService.Create(shop);
            return CreatedAtRoute("GetShop", new { id = shop._id.ToString() }, shop);
        }

    }

}
