using DotnetServer.Models;
using DotnetServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotnetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly ShipperService _shipperService;
        private readonly UserService _userService;
        public ShipperController(ShipperService shipperService, UserService userService)
        {
            _shipperService = shipperService;
            _userService = userService;
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
        public ActionResult<Shipper> Create(CreateShipperRequest request)
        {
           var newShipper = _shipperService.Create(request.shipper);
            User newUser = new();
            newUser.Username = request.Username;
            newUser.Password = request.Password;
            newUser.Type = request.Type;
            newUser.UserId = newShipper._id.ToString();
            _userService.Create(newUser);
            return CreatedAtRoute("GetShipper", new { id = newShipper._id.ToString() }, newShipper);
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

            return CreatedAtRoute("GetShipper", new { id = newShipper._id.ToString() }, newShipper);
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