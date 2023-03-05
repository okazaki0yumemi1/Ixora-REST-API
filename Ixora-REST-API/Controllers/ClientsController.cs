using Ixora_REST_API.ApiRoutes;
using Ixora_REST_API.Models;
using Ixora_REST_API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Ixora_REST_API.Controllers
{
    public class ClientsController : ControllerBase, IController
    {
        private readonly ClientsDbOperations _dbOperations;
        public ClientsController(ClientsDbOperations dbOperations)
        {
            _dbOperations = dbOperations;
        }
        [HttpGet(Routes.Clients.Get)]
        public async Task<IActionResult> GetByID([FromRoute] int clientId)
        {
            //var client = _clients.SingleOrDefault(x => x.Id == clientId);
            var client = await _dbOperations.GetByIDAsync(clientId);
            if (client == null) return NotFound();
            return Ok(client);
        }
        [HttpGet(Routes.Clients.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dbOperations.GetAllAsync());
        }
        [HttpPost(Routes.Clients.CreateClient)]
        public async Task<IActionResult> Create([FromBody] Client client)
        {
            //_clients.Add(client);
            await _dbOperations.CreateAsync(client);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = baseUrl + "/" + Routes.Clients.Get.Replace("{clientId}", client.Id.ToString());
            return Created(fullUrl, client);
        }

        [HttpPut(Routes.Clients.Update)]
        public async Task<IActionResult> Update([FromRoute] int clientId, [FromBody] Client client)
        {
            var newClient = new Client
            {
                Id = clientId,
                ClientName = client.ClientName,
                PhoneNumber = client.PhoneNumber
            };
            var updated = await _dbOperations.UpdateAsync(newClient);//_clients.SingleOrDefault(x => x.Id == clientId) != null;
            if (updated) { return Ok(newClient); }
            else return NotFound();
        }

        [HttpDelete(Routes.Clients.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int clientId)
        {
            var deleted = await _dbOperations.DeleteAsync(clientId);
            if (deleted) return NoContent();
            else return NotFound();
        }
    }
}