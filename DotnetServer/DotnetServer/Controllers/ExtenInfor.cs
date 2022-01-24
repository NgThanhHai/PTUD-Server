using DotnetServer.Models;
using DotnetServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotnetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtenInforController : ControllerBase
    {
        private readonly ExtenInforService _ExtenInforService;

        public ExtenInforController(ExtenInforService ExtenInforService)
        {
            _ExtenInforService = ExtenInforService;
        }

        [HttpGet]
        public ActionResult<List<ExtenInfor>> Get() =>
            _ExtenInforService.Get();

        [HttpGet("{id:length(24)}", Name = "GetExtenInfor")]
        public ActionResult<ExtenInfor> Get(string id)
        {
            var ExtenInfor = _ExtenInforService.Get(id);

            if (ExtenInfor == null)
            {
                return NotFound();
            }

            return ExtenInfor;
        }

        [HttpPost]
        public ActionResult<ExtenInfor> Create(ExtenInfor ExtenInfor)
        {
            _ExtenInforService.Create(ExtenInfor);

            return CreatedAtRoute("GetExtenInfor", new { id = ExtenInfor._id.ToString() }, ExtenInfor);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ExtenInfor newExtenInfor)
        {
            var ExtenInfor = _ExtenInforService.Get(id);

            if (ExtenInfor == null)
            {
                return NotFound();
            }

            _ExtenInforService.Update(id, newExtenInfor);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var ExtenInfor = _ExtenInforService.Get(id);

            if (ExtenInfor == null)
            {
                return NotFound();
            }

            _ExtenInforService.Remove(ExtenInfor._id);

            return NoContent();
        }
    }
}