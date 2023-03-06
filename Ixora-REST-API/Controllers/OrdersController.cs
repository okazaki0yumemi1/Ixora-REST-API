using Ixora_REST_API.Models;
using Ixora_REST_API.Persistence;
using Ixora_REST_API.ApiRoutes;
using Microsoft.AspNetCore.Mvc;
using static Ixora_REST_API.ApiRoutes.Routes;
using System.Drawing.Drawing2D;

namespace Ixora_REST_API.Controllers
{
    public class OrdersController : ControllerBase, IController<Order>
    {
        private readonly OrdersDbOperations _dbOperations;
        private readonly GoodsDbOperations _goodsDbOperations;
        public OrdersController(OrdersDbOperations dbOperations, GoodsDbOperations goodsDbOperations)
        {
            _dbOperations = dbOperations;
            _goodsDbOperations = goodsDbOperations;
        }
        [HttpPost(Routes.Orders.CreateOrder)]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            //Implement inventory check
            var orderDetails = order.OrderDetails.ToList();
            foreach (var thing in orderDetails)
            {
                var goodsRequest = await _goodsDbOperations.GetByIDAsync(thing.GoodsId);
                if (thing.Count > goodsRequest.LeftInStock) return BadRequest();
                else
                {
                    goodsRequest.LeftInStock -= thing.Count;
                    await _goodsDbOperations.UpdateAsync(goodsRequest);
                }
            }
            await _dbOperations.CreateAsync(order);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = baseUrl + "/" + Routes.Clients.Get.Replace("{orderId}", order.ID.ToString());
            return Created(fullUrl, order);
        }
        [HttpDelete(Routes.Orders.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var deleted = await _dbOperations.DeleteAsync(Id);
            if (deleted) return NoContent();
            else return NotFound();
        }
        [HttpGet(Routes.Orders.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dbOperations.GetAllAsync());
        }
        [HttpGet(Routes.Orders.Get)]
        public async Task<IActionResult> GetByID([FromRoute] int Id)
        {
            var order = await _dbOperations.GetByIDAsync(Id);
            if (order == null) return NotFound();
            return Ok(order);
        }
        [HttpPut(Routes.Orders.Update)]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] Order obj)
        {
            var newOrder = new Order
            {
                //ID = Id,
                ClientId = obj.ClientId,
                OrderDetails = obj.OrderDetails,
                //OrderDetailsId = obj.OrderDetailsId,
                IsComplete = obj.IsComplete,
            };
            var updated = await _dbOperations.UpdateAsync(newOrder);
            if (updated) { return Ok(newOrder); }
            else return NotFound();
        }

        //Implement method that returns all orders for a given client, sort by newest
    }
}
