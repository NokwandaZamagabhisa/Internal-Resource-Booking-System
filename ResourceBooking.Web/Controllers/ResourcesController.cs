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

        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Resources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResourceDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(dto);


            }

            var entity = await _resourceGateway.GetById(id);
            if (entity == null) return NotFound();

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Location = dto.Location;
            entity.IsAvailable = dto.IsAvailable;
            entity.Capacity = dto.Capacity;

            try
            {
                await _resourceGateway.Update(entity);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ResourceExistsAsync(dto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Resources/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = await _resourceGateway.GetById(id);
            if (resource != null)
            {
                await _resourceGateway.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ResourceExistsAsync(int id)
        {
            return await _resourceGateway.Exists(id);
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
