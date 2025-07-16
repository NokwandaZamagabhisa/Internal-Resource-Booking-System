using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceBooking.Core.Entities;
using ResourceBooking.Core.Gateways;
using ResourceBooking.Web.Models;

namespace ResourceBooking.Web.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly IResourceGateway _resourceGateway;

        public ResourcesController(IResourceGateway resourceGateway)
        {
            _resourceGateway = resourceGateway;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            var resources = await _resourceGateway.GetAll();
            var dtos = resources.Select(e => MapResourceToDto(e)).ToList();
            return View(dtos);
        }

        private static ResourceDto MapResourceToDto(Resource e)
        {
            return new ResourceDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Location = e.Location,
                IsAvailable = e.IsAvailable,
                Capacity = e.Capacity
            };
        }

 
     

        

      
    }
}
