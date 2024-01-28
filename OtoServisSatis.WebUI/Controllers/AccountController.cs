using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.Models;
using System.Security.Claims;

namespace OtoServisSatis.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IService<Kullanici> _service;//repository pattern faydaları servis bu tarafta kullanıcı için çalışırken diğer controller da rol için çalışıyor
		private readonly IService<Rol> _serviceRol;
		public AccountController(IService<Kullanici> service, IService<Rol> serviceRol)
		{
			_service = service;
			_serviceRol = serviceRol;
		}


		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RegisterAsync(Kullanici kullanici)
		{
			    
                var rol = await _serviceRol.GetAsync(r => r.Adi == "Customer");
				if (rol == null)
				{
					ModelState.AddModelError("", "kayıt başarısız");
					return View();
				}
				kullanici.RolId = rol.Id;
				kullanici.AktifMi=true; 
				await _service.AddAsync(kullanici); //asenkron metot halini kullanarak daha performanslı çalışmasını sağlıyoruz
				await _service.SaveAsync();
				return RedirectToAction(nameof(Index)); //işlem sonunda bizi tekrardan Indexe yönlendiriyor
			
				//ModelState.AddModelError("", "bir sorun oluştu");


			    //return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LoginAsync(CustomerLoginViewModel loginViewModel)
		{
            try
            {

                var account = await _service.GetAsync(k => k.Email == loginViewModel.Email && k.Sifre == loginViewModel.Sifre && k.AktifMi == true);
                if (account == null)
                {
                    ModelState.AddModelError("", "giriş başarısız");
                }
                else
                {
                    var rol = _serviceRol.Get(r => r.Id == account.Id);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,account.Adi),
                    };

                    if (rol != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, rol.Adi));
                    }
                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "hata oluştu");
            }
            return View();
        }


	}
}
