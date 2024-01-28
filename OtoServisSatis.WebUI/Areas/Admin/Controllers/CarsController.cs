using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.Utils;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
	[Area("Admin"), ]
	public class CarsController : Controller
    {
        // GET: CarsController
        private readonly ICarService _service;
        private readonly IService<Marka> _serviceMarka;

        

        public CarsController(ICarService service, IService<Marka> serviceMarka)
        {
            _service = service;
            _serviceMarka = serviceMarka;
        }

        public async Task<ActionResult> IndexAsync()
        {
            var model=await _service.GetCustomCarList();
            ViewBag.MarkaId = new SelectList(_serviceMarka.GetAll(), "Id", "Adi");
            return View(model);
        }

        // GET: CarsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarsController/Create
        public ActionResult Create()
        {
            ViewBag.MarkaId= new SelectList(_serviceMarka.GetAll(), "Id", "Adi");
            return View();
        }

        // POST: CarsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Arac arac,IFormFile? Resim1, IFormFile? Resim2, IFormFile? Resim3)
        {
            try
            {
                arac.Resim1=await FileHelper.FileLoaderAsync(Resim1,"/Img/Cars/");
                arac.Resim2=await FileHelper.FileLoaderAsync(Resim2,filePath: "/Img/Cars/");
                arac.Resim3=await FileHelper.FileLoaderAsync(Resim3, "/Img/Cars/");
                await _service.AddAsync(arac);
                await _service.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "bir hata oluştu");
            }
            return View(arac);
        }

        // GET: CarsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model=await _service.FindAsync(id);
            ViewBag.MarkaId = new SelectList(_serviceMarka.GetAll(), "Id", "Adi");
            return View(model);
        }

        // POST: CarsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Arac arac, IFormFile? Resim1, IFormFile? Resim2, IFormFile? Resim3)
        {
            try
            {
                if(Resim1 is not null)
                {
                    arac.Resim1 = await FileHelper.FileLoaderAsync(Resim1, "/Img/Cars/");
                   
                }
                if (Resim2 is not null)
                {
                   
                    arac.Resim2 = await FileHelper.FileLoaderAsync(Resim2, "/Img/Cars/");
                    
                }
                if (Resim3 is not null)
                {
                    arac.Resim3 = await FileHelper.FileLoaderAsync(Resim3, "/Img/Cars/");
                }
                _service.Update(arac);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "bir hata oluştu");
            }
            return View(arac);
        }

        // GET: CarsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model=await _service.FindAsync(id);
            return View(model);
        }

        // POST: CarsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Arac arac)
		{
			try
            {
				 _service.Delete(arac);
                 await _service.SaveAsync();
                 return RedirectToAction(nameof(Index));
            }
            catch
            {
				return View();
			}

		}
    }
}
