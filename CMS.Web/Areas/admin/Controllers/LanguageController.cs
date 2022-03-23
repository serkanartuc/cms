using AutoMapper;
using CMS.Web.Areas.admin.Models;
using CMS.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.admin.Controllers
{
    public class LanguageController : AdminBaseController
    {
        private readonly CmsDbContext _ctx;
        private readonly IMapper _mapper;


        public LanguageController(CmsDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
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
        public IActionResult Create(LanguageModel language)
        {
            if (!ModelState.IsValid)
            {
                AddModelValidNotifications();

                return View(language);                
            }

            try
            {
                var dbLanguage = _mapper.Map<Language>(language);

                _ctx.Languages.Add(dbLanguage);

                _ctx.SaveChanges();

                AddNotification("Kayıt başarıyla oluşturuldu!", Helpers.NotificationType.Success);
            }
            catch
            {
                AddNotification("İşlem sırasında bir hata oluştu!", Helpers.NotificationType.Error);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }

            var language = _ctx.Languages.FirstOrDefault(l => l.Id == id);

            if(language == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<LanguageModel>(language);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LanguageModel language)
        {
            if (!ModelState.IsValid)
            {
                AddModelValidNotifications();

                return View(language);
            }

            var dbLanguage = _ctx.Languages.FirstOrDefault(l => l.Id == language.Id);

            if(dbLanguage == null)
            {
                AddNotification("Kayıt bulunamadı!", Helpers.NotificationType.Error);

                return View(language);
            }

            try
            {
                 dbLanguage = _mapper.Map(language, dbLanguage);

                _ctx.Languages.Update(dbLanguage);

                _ctx.SaveChanges();

                AddNotification("Kayıt başarıyla düzenlendi!", Helpers.NotificationType.Success);
            }
            catch
            {
                AddNotification("İşlem sırasında bir hata oluştu!", Helpers.NotificationType.Error);
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var language = _ctx.Languages.FirstOrDefault(l => l.Id == id);

            if(language == null)
            {
                return Json(new { success = false, message = "Kayıt bulunamadı!" });
            }

            try
            {
                _ctx.Languages.Remove(language);

                _ctx.SaveChanges();

                return Json(new { success = true, message = "Kayıt başarıyla silindi!" });

            }
            catch
            {
                return Json(new { success = false, message = "İşlem sırasında bir hata oluştu!" });
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var languages = _ctx.Languages.ToList();

            return Json(new { data= languages});
        }

    }
}
