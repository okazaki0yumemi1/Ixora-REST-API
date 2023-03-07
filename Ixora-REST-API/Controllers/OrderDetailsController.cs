using Ixora_REST_API.ApiRoutes;
using Ixora_REST_API.Models;
using Ixora_REST_API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Ixora_REST_API.ApiRoutes.Routes;

namespace Ixora_REST_API.Controllers
{
    [ApiController]
    public class OrderDetailsController : ControllerBase, IController<OrderDetails>
    {
        private readonly OrderDetailsDbOperations _dbOperations;
        public OrderDetailsController(OrderDetailsDbOperations dbOperations)
        {
            _dbOperations = dbOperations;
        }
        [HttpPost(Routes.Details.CreateDetails)]
        public async Task<IActionResult> Create([FromBody] OrderDetails obj)
        {
            await _dbOperations.CreateAsync(obj);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = baseUrl + "/" + Routes.Orders.Get.Replace("{orderId}", obj.Order.ID.ToString() + "/details/" + obj.Id);
            return Created(fullUrl, obj);
            //This should work, but I have to test it later
        }
        [HttpDelete(Routes.Details.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var deleted = await _dbOperations.DeleteAsync(Id);
            if (deleted) return NoContent();
            else return NotFound();
        }
        [HttpDelete(Routes.Details.GetAll + "/meow")]
        public async Task<IActionResult> GetAll()
        {
            return NotFound(); //it does not make any sense
        }

        [HttpGet(Routes.Details.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] int orderId)
        {
            var result = await _dbOperations.GetAllAsync(orderId);
            if (result.IsNullOrEmpty()) return Ok(result);
            else return NotFound();
        }
        [HttpGet(Routes.Details.Get)]
        public async Task<IActionResult> GetByID([FromRoute] int Id)
        {
            var details = await _dbOperations.GetByIDAsync(Id);
            if (details == null) return NotFound();
            return Ok(details);
        }
        [HttpPut(Routes.Details.Update)]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] OrderDetails obj)
        {
            var newDetails = new OrderDetails
            {
                //Id = Id,
                Count = obj.Count,
                Goods = obj.Goods,
                GoodsId = obj.GoodsId,
                ItemPrice = obj.ItemPrice,
            };
            var updated = await _dbOperations.UpdateAsync(newDetails);
            if (updated) { return Ok(newDetails); }
            else return NotFound();
        }
    }
}
