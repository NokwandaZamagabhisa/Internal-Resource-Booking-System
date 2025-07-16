//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ResourceBooking.Core.Entities;
//using ResourceBooking.Core.Gateways;

//namespace ResourceBooking.Controllers
//{
//    public class ResourcesCopyController : Controller
//    {
//        private readonly IResourceGateway _resourceGateway;

//        public ResourcesCopyController(IResourceGateway resourceGateway)
//        {
//            _resourceGateway = resourceGateway;
//        }

//        // GET: Resources
//        public async Task<IActionResult> Index()
//        {
//            var resources = await _resourceGateway.GetAll();
//            return View(resources);
//        }

//        // GET: Resources/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var resource = await _resourceGateway.GetById(id.Value);
//            if (resource == null)
//            {
//                return NotFound();
//            }

//            return View(resource);
//        }

//        // GET: Resources/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Resources/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Create([Bind("Id,Name,Description,IsAvailable,Capacity")] Resource resource)

//        public async Task<IActionResult> Create(Resource resource)
//        {
//            if (ModelState.IsValid)
//            {
//                await _resourceGateway.Add(resource);

//                return RedirectToAction(nameof(Index));
//            }
//            return View(resource);
//        }

//        // GET: Resources/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var resource = await _resourceGateway.GetById(id.Value);
//            if (resource == null)
//            {
//                return NotFound();
//            }
//            return View(resource);
//        }

//        // POST: Resources/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsAvailable,Capacity")] Resource resource)

//        public async Task<IActionResult> Edit(int id, Resource resource)
//        {
//            if (id != resource.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await _resourceGateway.Update(resource);

//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (! await ResourceExistsAsync(resource.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(resource);
//        }

//        // GET: Resources/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var resource = await _resourceGateway.GetById(id.Value);
//            if (resource == null)
//            {
//                return NotFound();
//            }

//            return View(resource);
//        }

//        // POST: Resources/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var resource = await _resourceGateway.GetById(id);
//            if (resource != null)
//            {
//                await _resourceGateway.Delete(id);
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        private async Task<bool> ResourceExistsAsync(int id)
//        {
//            return await _resourceGateway.Exists(id);
//        }
//    }
//}
