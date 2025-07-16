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

        // GET: Resources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var entity = new Resource
            {
                Name = dto.Name,
                Description = dto.Description,
                Location = dto.Location,
                IsAvailable = dto.IsAvailable,
                Capacity = dto.Capacity
            };
            await _resourceGateway.Add(entity);

            return RedirectToAction(nameof(Index));
        }

        // GET: Resources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _resourceGateway.GetById(id.Value);
            if (resource == null)
            {
                return NotFound();
            }

            var dto = MapResourceToDto(resource);
            return View(dto);
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
