using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
   [Area("Admin")]
    public class UsersController : Controller
    {
        
        // GET: UsersController
        private readonly IService<Kullanici> _service;//repository pattern faydaları servis bu tarafta kullanıcı için çalışırken diğer controller da rol için çalışıyor
        private readonly IService<Rol> _serviceRol;
        public UsersController(IService<Kullanici> service, IService<Rol> serviceRol) //dependency injection işlemi Kullanici classı için çalışsın
        {
            _service = service;
            _serviceRol = serviceRol;
        }

        public async Task<ActionResult> IndexAsync() //ana sayfa açıldığında kullanıcıları getirecek ana view
        {
            var model = await _service.GetAllAsync();
            ViewBag.RolId = new SelectList(_serviceRol.GetAll(), "Id", "Adi");
            return View(model);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create() //bu create in get metodu aşağıdakide set metodu
        {
            //ViewBag RolId yi view kısmında asp item da isteniyor
            ViewBag.RolId =new SelectList(_serviceRol.GetAll(), "Id","Adi"); //veritabanındaki kolon adları yani Rol classındaki propertiesler
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kullanici kullanici)
        {
            
                try
                {
                    _service.Add(kullanici); //asenkron metot halini kullanarak daha performanslı çalışmasını sağlıyoruz
                    _service.Save();
                    return RedirectToAction(nameof(Index)); //işlem sonunda bizi tekrardan Indexe yönlendiriyor
                }
                catch
                {
                ModelState.AddModelError("", "bir sorun oluştu");


                }
                return View(kullanici);
            // ViewBag.RolId = new SelectList(_serviceRol.GetAll(), "Id", "Adi");

        }
            

        

        // GET: UsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model=await _service.FindAsync(id);
            ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Kullanici kullanıcı)
        {
            
                try
                {

                    _service.Update(kullanıcı); //asenkron metot halini kullanarak daha performanslı çalışmasını sağlıyoruz
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index)); //işlem sonunda bizi tekrardan Indexe yönlendiriyor
                }
                catch
                {
                ModelState.AddModelError("", "bir hata oluştu");

                }
                return View(kullanıcı);
            //ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");



        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model=await _service.FindAsync(id);
            return View(model);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Kullanici kullanici)
        {
            try
            {
                _service.Delete(kullanici);
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
