using Ixora_REST_API.ApiRoutes;
using Ixora_REST_API.Models;
using Ixora_REST_API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Ixora_REST_API.Controllers
{
    public class ClientsController : ControllerBase
    {
        private List<Models.Client> _clients;
        public ClientsController()
        {
            _clients = new List<Models.Client>();
            _clients.Add(new Models.Client());
        }
        [HttpGet(Routes.Clients.Get)]
        public IActionResult GetClient([FromRoute] Guid clientId)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientId);
            if (client == null) return NotFound();
            return Ok(client);
        }
        [HttpGet(Routes.Clients.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_clients);
        }
        [HttpPost(Routes.Clients.CreateClient)]
        public IActionResult Create([FromBody] Client client)
        {
            _clients.Add(client);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var fullUrl = baseUrl + "/" + Routes.Clients.Get.Replace("{clientId}", client.Id.ToString());
            return Created(fullUrl, client);
        }

        [HttpPut(Routes.Clients.Update)]
        public IActionResult Update ([FromRoute] Guid clientId, [FromBody] Client client)
        {
            var newClient = new Client
            {
                Id = clientId,
                ClientName = client.ClientName,
                PhoneNumber = client.PhoneNumber
            };
            bool exists = _clients.SingleOrDefault(x => x.Id == clientId) != null;
            if (exists)
            {
                var index = _clients.FindIndex(x => x.Id == clientId);
                _clients[index] = newClient;
                return Ok(newClient);
            }
            else return NotFound();
        }

        [HttpDelete(Routes.Clients.Delete)]
        public IActionResult Delete([FromRoute] Guid clientId)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientId);
            if (client == null) return NotFound(); 
            else 
            {
                _clients.Remove(client);
                return Ok(client);
            }
        }
    }
}