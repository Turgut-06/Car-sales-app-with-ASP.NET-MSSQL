using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Controllers
{
    public class AracController : Controller
    {
        private readonly ICarService _serviceArac;
		private readonly IService<Musteri> _serviceMusteri;

		public AracController(ICarService serviceArac, IService<Musteri> serviceMusteri)
		{
			_serviceArac = serviceArac;
			_serviceMusteri = serviceMusteri;
		}

		public async Task<IActionResult> IndexAsync(int id)
        {
            var model=await _serviceArac.GetCustomCar(id);
            return View(model);
        }

        [Route("Satılık Tüm Araçlar")]
		public async Task<IActionResult> List()
		{
			var model = await _serviceArac.GetCustomCarList(s => s.SatistaMi);
			return View(model);
		}
        public async Task<IActionResult> Ara(string q)
        {
            var model = await _serviceArac.GetCustomCarList(s => s.SatistaMi && s.marka.Adi.Contains(q) || s.KasaTipi.Contains(q) || s.Modeli.Contains(q));
            return View(model);
        }

        [HttpPost]
		public async Task<IActionResult> MusteriKayit(Musteri musteri)
		{
			try
			{
				await _serviceMusteri.AddAsync(musteri);
                await _serviceMusteri.SaveAsync();
				return Redirect("/Arac/Index/" + musteri.AracId);
			}
			catch 
			{

				ModelState.AddModelError("", "bir hata oluştu");
			}
			return View();
			
		}
	}
}
