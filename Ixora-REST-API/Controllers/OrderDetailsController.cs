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
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost(Routes.Details.CreateDetails)]
        public async Task<IActionResult> Create([FromBody] OrderDetails obj)
        {
            throw new NotImplementedException();
        }
        //[HttpPost(Routes.Details.CreateDetails)]
        //public async Task<IActionResult> Create([FromBody] OrderDetails obj)
        //{
        //    var newDetails = new OrderDetails();

        //    newDetails.ItemPrice = obj.ItemPrice;
        //    newDetails.Count = obj.Count;
        //    newDetails.GoodsId = obj.GoodsId;

        //    await _dbOperations.CreateAsync(newDetails);
        //    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        //    var fullUrl = baseUrl + "/" + Routes.Orders.Get.Replace("{orderId}", obj.OrderId.ToString() + "/details/" + obj.Id);
        //    return Created(fullUrl, obj);
        //}
        [HttpDelete(Routes.Details.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int detailsId)
        {
            var deleted = await _dbOperations.DeleteAsync(detailsId);
            if (deleted) return NoContent();
            else return NotFound();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete(Routes.Details.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return NotFound();
        }

        [HttpGet(Routes.Details.GetAll)]
        public async Task<IActionResult> GetAll([FromRoute] int orderId)
        {
            var result = await _dbOperations.GetAllAsync(orderId);
            if (result.IsNullOrEmpty()) return NotFound();
            else return Ok(result);
        }
        [HttpGet(Routes.Details.Get)]
        public async Task<IActionResult> GetByID([FromRoute] int detailsId)
        {
            var details = await _dbOperations.GetByIDAsync(detailsId);
            if (details == null) return NotFound();
            return Ok(details);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut(Routes.Details.Update)]
        public async Task<IActionResult> Update([FromRoute] int detailsId, [FromBody] OrderDetails obj)
        {
            if ((obj.Count < 0) || (obj.ItemPrice < 0)) return BadRequest();
            var details = await _dbOperations.GetByIDAsync(detailsId);
            if (details == null) return NotFound();
            //details.ItemPrice = obj.ItemPrice; //not sure if it would be useful at all
            details.Count = obj.Count;
            var updated = await _dbOperations.UpdateAsync(details);
            if (updated) { return Ok(details); }
            else return NotFound();
        }
    }
}
