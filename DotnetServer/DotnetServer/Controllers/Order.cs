using DotnetServer.Models;
using DotnetServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotnetServer.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService shipperService)
        {
            _orderService = shipperService;
        }

        [ActionName("GetAll")]
        [HttpPost]
        public ActionResult<List<OrderResponse>> Get(orderBodyRequest request) =>
            _orderService.Get(request);
        [ActionName("GetOTP")]
        [HttpPost]
        public ActionResult<string> GetOTP(GetOTPRequest request)
        {
            var sendFail = _orderService.SendMailGoogleSmtp(request.phone);
            return sendFail.Result;
          
        } 
        [ActionName("VerifyOTP")]
        [HttpPost]
        public ActionResult<bool> VerifyOTP(VerifyOTPRequest request)
        {
            return _orderService.VerifyOTP(request.id, request.otp);


        }
        [ActionName("GetDetail")]
        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<OrderResponse> Get(string id)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [ActionName("Create")]
        [HttpPost]
        public ActionResult<Order> Create(Order shipper)
        {
            _orderService.Create(shipper);

            return CreatedAtRoute("GetOrder", new { id = shipper._id.ToString() }, shipper);
        }

        [ActionName("Update")]
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Order newOrder)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.Update(id, newOrder);

            return CreatedAtRoute("GetOrder", new { id = newOrder._id.ToString() }, newOrder);
        }
        [ActionName("UpdateStatus")]
        [HttpPut]
        public IActionResult UpdateStatus(string id, int? status)
        {
            if (status == null || status < -3 || status > 3)
            {
                return NotFound();
            }
            var order = _orderService.GetOrigin(id);

            if (order == null)
            {
                return NotFound();
            }

            var newOrder = order;
            newOrder.status = status;
            _orderService.Update(id, newOrder);

            return CreatedAtRoute("GetOrder", new { id = newOrder._id.ToString() }, newOrder);
        }

        [ActionName("Delete")]
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var shipper = _orderService.Get(id);

            if (shipper == null)
            {
                return NotFound();
            }

            _orderService.Remove(shipper._id);

            return NoContent();
        }
        
    }
}