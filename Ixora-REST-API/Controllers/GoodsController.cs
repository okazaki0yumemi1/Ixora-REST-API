using Ixora_REST_API.ApiRoutes;
using Ixora_REST_API.Models;
using Ixora_REST_API.Persistence;
using Microsoft.AspNetCore.Mvc;
using static Ixora_REST_API.ApiRoutes.Routes;

namespace Ixora_REST_API.Controllers
{
    [ApiController]
    public class GoodsController : ControllerBase, IController<Models.Goods>
    {
        private readonly GoodsDbOperations _dbOperations;
        public GoodsController(GoodsDbOperations dbOperations)
        {
            _dbOperations = dbOperations;
        }
        [HttpPost(Routes.Goods.Create)]
        public async Task<IActionResult> Create([FromBody] Models.Goods obj)
        {
            await _dbOperations.CreateAsync(obj);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = baseUrl + "/" + Routes.Goods.Get.Replace("{goodsId}", obj.Id.ToString());
            return Created(fullUrl, obj);
        }
        [HttpDelete(Routes.Goods.Delete)]
        public async Task<IActionResult> Delete(/*[FromRoute] */int Id)
        {
            var deleted = await _dbOperations.DeleteAsync(Id);
            if (deleted) return NoContent();
            else return NotFound();
        }
        [HttpGet(Routes.Goods.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dbOperations.GetAllAsync());
        }
        [HttpGet(Routes.Goods.Get)]
        public async Task<IActionResult> GetByID([FromRoute] int Id)
        {
            var goods = await _dbOperations.GetByIDAsync(Id);
            if (goods == null) return NotFound();
            return Ok(goods);
        }
        [HttpPut(Routes.Goods.Update)]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] Models.Goods obj)
        {
            var newThing = new Models.Goods
            {
                //Id = Id,
                //GoodsType = obj.GoodsType,
                //GoodsTypeID = obj.GoodsTypeID,
                LeftInStock = obj.LeftInStock,
                Name = obj.Name,
            };
            var updated = await _dbOperations.UpdateAsync(newThing);
            if (updated) { return Ok(newThing); }
            else return NotFound();
        }
    }
}
