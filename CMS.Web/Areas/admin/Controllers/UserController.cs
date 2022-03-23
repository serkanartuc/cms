using AutoMapper;
using CMS.Web.Areas.admin.Models;
using CMS.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Web.Areas.admin.Controllers
{
    public class UserController : AdminBaseController
    {
        private readonly IMapper _mapper;

        private readonly CmsDbContext _ctx;

        public UserController(IMapper mapper, CmsDbContext ctx)
        {
            _mapper = mapper;
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel user)
        {
            var userEmail = _ctx.Users.FirstOrDefault(u => u.Email == user.Email);

            if (!ModelState.IsValid)
            {
                AddModelValidNotifications();

                if (userEmail != null)
                {
                    this.AddNotification("Bu e-posta adresi kullanılmaktadır!", Helpers.NotificationType.Error);
                }

                return View(user);
            }

            if (userEmail != null)
            {
                this.AddNotification("Bu e-posta adresi kullanılmaktadır!", Helpers.NotificationType.Error);

                return View(user);
            }

            try
            {
                var dbUser = _mapper.Map<User>(user);

                _ctx.Users.Add(dbUser);

                _ctx.SaveChanges();

                this.AddNotification("Kayıt başarıyla oluşturuldu!", Helpers.NotificationType.Success);
            }
            catch
            {
                this.AddNotification("İşlem sırasında bir hata oluştu!", Helpers.NotificationType.Error);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var user = _ctx.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<UserEditModel>(user);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditModel user)
        {

            var userEmail = _ctx.Users.FirstOrDefault(u => u.Id != user.Id && u.Email == user.Email);

            if (!ModelState.IsValid)
            {
                AddModelValidNotifications();

                if (userEmail != null)
                {
                    this.AddNotification("Bu e-posta adresi kullanılmaktadır!", Helpers.NotificationType.Error);
                }

                return View(user);
            }

            if (userEmail != null)
            {
                this.AddNotification("Bu e-posta adresi kullanılmaktadır!", Helpers.NotificationType.Error);

                return View(user);
            }

            var dbUser = _ctx.Users.FirstOrDefault(p => p.Id == user.Id);

            if (dbUser == null)
            {
                this.AddNotification("Kayıt bulunamadı!", Helpers.NotificationType.Error);

                return View(user);
            }

            try
            {
                dbUser = _mapper.Map(user, dbUser);

                _ctx.Users.Update(dbUser);                

                _ctx.SaveChanges();

                this.AddNotification("Kayıt başarıyla düzenlendi!", Helpers.NotificationType.Success);
            }
            catch
            {
                this.AddNotification("İşlem sırasında bir hata oluştu!", Helpers.NotificationType.Error);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = _ctx.Users.FirstOrDefault(u => u.Id == id);

            var userCount = _ctx.Users.Count();

            if (user == null)
            {
                return Json(new { success = false, message = "Kayıt bulunamadı!" });
            }

            if (userCount == 1)
            {
                return Json(new { success = false, message = "Son kullanıcı silinemez!" });
            }

            try
            {
                _ctx.Users.Remove(user);

                _ctx.SaveChanges();

                return Json(new { success = true, message = "Kayıt başarıyla silindi." });
            }
            catch
            {
                return Json(new { success = false, message = "İşlem sırasında bir hata oluştu." });
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _ctx.Users.ToList();

            var model = new UserViewModel();

            model.Users = _mapper.Map<List<UserModel>>(users);

            return Json(new { data = users });
        }

    }
}
