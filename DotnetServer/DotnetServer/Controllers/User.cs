using DotnetServer.Models;
using DotnetServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotnetServer.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ShipperService _shipperService;
        private readonly UserService _userService;
        public UserController(ShipperService shipperService, UserService userService)
        {
            _shipperService = shipperService;
            _userService = userService;
        }

        [ActionName("GetAll")]
        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        [ActionName("Get")]
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        [ActionName("Login")]
        [HttpPost]
        public ActionResult<string> LogIn(string Username, string Password)
        {
            var user = _userService.Get(Username, Password);
            if (user == null)
            {
                return NotFound();
            }
            return user._id.ToString();
        }

        [ActionName("Update")]
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User newUser)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Update(id, newUser);

            return CreatedAtRoute("GetShipper", new { id = newUser._id.ToString() }, newUser);
        }

        [ActionName("Delete")]
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user._id);

            return NoContent();
        }
    }
}