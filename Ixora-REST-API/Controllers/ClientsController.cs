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
    }
}