using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
	[Area("Admin"), ]
	public class SalesController : Controller
    {
        // GET: SalesController
        private readonly IService<Satis> _service;
        private readonly IService<Arac> _serviceArac;
        private readonly IService<Musteri> _serviceMusteri;



        public SalesController(IService<Satis> service, IService<Arac> serviceArac, IService<Musteri> serviceMusteri)
        {
            _service = service;
            _serviceArac = serviceArac;
            _serviceMusteri = serviceMusteri;
        }

        public async Task<ActionResult> IndexAsync()
        {
            var model=await _service.GetAllAsync();
            ViewBag.AracId=new SelectList(_serviceArac.GetAll(), "Id", "Modeli");
            ViewBag.MusteriId= new SelectList(_serviceMusteri.GetAll(), "Id", "Adi");
            return View(model);
        }

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesController/Create
        public ActionResult Create()
        {
            ViewBag.AracId = new SelectList(_serviceArac.GetAll(), "Id", "Modeli");
            ViewBag.MusteriId = new SelectList(_serviceMusteri.GetAll(), "Id", "Adi");
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Satis satis)
        {
            try
            {
                await _service.AddAsync(satis);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "bir hata oluştu");
            }
            return View(satis);
        }

        // GET: SalesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model=await _service.FindAsync(id);
            ViewBag.AracId = new SelectList(_serviceArac.GetAll(), "Id", "Modeli");
            ViewBag.MusteriId = new SelectList(_serviceMusteri.GetAll(), "Id", "Adi");
            return View(model);
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Satis satis)
        {
            try
            {
                _service.Update(satis);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "bir hata oluştu");
            }
            return View(satis);
        }

        // GET: SalesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model=await _service.FindAsync(id);   
            return View(model);
        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Satis satis)
        {
            try
            {
                _service.Delete(satis); 
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
