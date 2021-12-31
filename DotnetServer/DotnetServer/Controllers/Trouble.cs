using DotnetServer.Models;
using DotnetServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotnetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TroubleController : ControllerBase
    {
        private readonly TroubleService _TroubleService;

        public TroubleController(TroubleService TroubleService)
        {
            _TroubleService = TroubleService;
        }

        [HttpGet]
        public ActionResult<List<Trouble>> Get() =>
            _TroubleService.Get();

        [HttpGet("{id:length(24)}", Name = "GetTrouble")]
        public ActionResult<Trouble> Get(string id)
        {
            var Trouble = _TroubleService.Get(id);

            if (Trouble == null)
            {
                return NotFound();
            }

            return Trouble;
        }

        [HttpPost]
        public ActionResult<Trouble> Create(Trouble Trouble)
        {
            _TroubleService.Create(Trouble);

            return CreatedAtRoute("GetTrouble", new { id = Trouble._id.ToString() }, Trouble);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Trouble newTrouble)
        {
            var Trouble = _TroubleService.Get(id);

            if (Trouble == null)
            {
                return NotFound();
            }

            _TroubleService.Update(id, newTrouble);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var Trouble = _TroubleService.Get(id);

            if (Trouble == null)
            {
                return NotFound();
            }

            _TroubleService.Remove(Trouble._id);

            return NoContent();
        }
    }
}