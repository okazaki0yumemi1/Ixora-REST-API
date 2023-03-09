using Ixora_REST_API.ApiRoutes;
using Ixora_REST_API.Models;
using Ixora_REST_API.Persistence;
using Microsoft.AspNetCore.Mvc;
using static Ixora_REST_API.ApiRoutes.Routes;

namespace Ixora_REST_API.Controllers
{
    [ApiController]
    public class GoodsTypeController : ControllerBase, IController<Models.GoodsType>
    {
        private readonly GoodsTypeDbOperations _dbOperations;
        public GoodsTypeController(GoodsTypeDbOperations dbOperations)
        {
            _dbOperations = dbOperations;
        }
        [HttpPost(Routes.GoodsTypes.Create)]
        public async Task<IActionResult> Create([FromBody] GoodsType obj)
        {
            await _dbOperations.CreateAsync(obj);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = baseUrl + "/" + Routes.GoodsTypes.Get.Replace("{goodsTypeId}", obj.ID.ToString());
            return Created(fullUrl, obj);
        }
        [HttpDelete(Routes.GoodsTypes.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int goodsTypeId)
        {
            var deleted = await _dbOperations.DeleteAsync(goodsTypeId);
            if (deleted) return NoContent();
            else return NotFound();
        }
        [HttpGet(Routes.GoodsTypes.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dbOperations.GetAllAsync());
        }
        [HttpGet(Routes.GoodsTypes.Get)]
        public async Task<IActionResult> GetByID([FromRoute] int goodsTypeId)
        {
            var goodsType = await _dbOperations.GetByIDAsync(goodsTypeId);
            if (goodsType == null) return NotFound();
            return Ok(goodsType);
        }
        [HttpPut(Routes.GoodsTypes.Update)]
        public async Task<IActionResult> Update([FromRoute] int goodsTypeId, [FromBody] GoodsType obj)
        {
            if (obj.GroupName == string.Empty) return BadRequest();
            var group = await _dbOperations.GetByIDAsync(goodsTypeId);
            group.GroupName = obj.GroupName;
            var updated = await _dbOperations.UpdateAsync(group);
            if (updated) { return Ok(group); }
            else return NotFound();
        }
    }
}
