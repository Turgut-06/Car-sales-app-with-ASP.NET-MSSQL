using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CustomersController : Controller
    {
        // GET: CustomersController
        private readonly IService<Musteri> _service;
        private readonly IService<Arac> _serviceArac;

        public CustomersController(IService<Musteri> service, IService<Arac> serviceArac)
        {
            _service = service;
            _serviceArac = serviceArac;
        }

        public async Task<ActionResult> IndexAsync()
        {
            var model=await _service.GetAllAsync();
            ViewBag.AracId= new SelectList(_serviceArac.GetAll(), "Id", "Modeli"); //arkada Id si tutuluyor önde Modeli gözüküyor
            return View(model);
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            ViewBag.AracId = new SelectList(_serviceArac.GetAll(), "Id", "Modeli");
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Musteri musteri)
        {
            try
            {
                await _service.AddAsync(musteri);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "bir hata oluştu");
            }
            return View(musteri);
        }

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model=await _service.FindAsync(id);
            ViewBag.AracId = new SelectList(_serviceArac.GetAll(), "Id", "Modeli");
            return View(model);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Musteri musteri)
        {
            try
            {
                _service.Update(musteri);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "hata oluştu");
            }
            return View(musteri);
        }

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model=await _service.FindAsync(id);
            return View(model);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Musteri musteri)
        {
            try
            {
                _service.Delete(musteri);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            
        }
    }
}
