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
            if ((obj.Name == string.Empty) || (obj.Price < 0) || (obj.LeftInStock < 0)) return BadRequest();
            await _dbOperations.CreateAsync(obj);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = baseUrl + "/" + Routes.Goods.Get.Replace("{goodsId}", obj.Id.ToString());
            return Created(fullUrl, obj);
        }
        [HttpDelete(Routes.Goods.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int goodsId)
        {
            var deleted = await _dbOperations.DeleteAsync(goodsId);
            if (deleted) return NoContent();
            else return NotFound();
        }
        [HttpGet(Routes.Goods.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dbOperations.GetAllAsync());
        }
        [HttpGet(Routes.Goods.Get)]
        public async Task<IActionResult> GetByID([FromRoute] int goodsId)
        {
            var goods = await _dbOperations.GetByIDAsync(goodsId);
            if (goods == null) return NotFound();
            return Ok(goods);
        }
        [HttpPut(Routes.Goods.Update)]
        public async Task<IActionResult> Update([FromRoute] int goodsId, [FromBody] Models.Goods obj)
        {
            if ((obj.Price < 0) || (obj.Name == string.Empty) || (obj.LeftInStock < 0)) return BadRequest();
            var thing = await _dbOperations.GetByIDAsync(goodsId);
            if (thing == null) return NotFound();
            thing.LeftInStock = obj.LeftInStock;
            thing.Name = obj.Name;
            var updated = await _dbOperations.UpdateAsync(thing);
            if (updated) { return Ok(thing); }
            else return NotFound();
        }
        [HttpGet(Routes.Goods.GetAllAvailable)]
        public async Task<IActionResult> GetAllAvailable([FromRoute] bool isInStock)
        {
            var inventory = await _dbOperations.GetAllAsync();
            inventory.RemoveAll(x => x.LeftInStock > 0 != isInStock);
            if (inventory.Count == 0) return NoContent();
            else return Ok(inventory);
        }
    }
}
