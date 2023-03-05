using Ixora_REST_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ixora_REST_API.Controllers
{
    public interface IController
    {
        Task<IActionResult> Create<T>(T obj) where T : Models.Entity;
        Task<IActionResult> Delete(int Id);
        Task<IActionResult> GetAll();
        Task<IActionResult> GetByID(int Id);
        Task<IActionResult> Update<T>(int Id, T obj) where T : Models.Entity;
    }
}