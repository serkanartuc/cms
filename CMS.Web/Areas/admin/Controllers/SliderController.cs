using AutoMapper;
using CMS.Web.Areas.admin.Models;
using CMS.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CMS.Web.Areas.admin.Controllers
{
    public class SliderController : AdminBaseController
    {
        private readonly CmsDbContext _ctx;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public SliderController(CmsDbContext ctx, IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            _ctx = ctx;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            SliderModel sliderModel = new()
            {
                LanguageList = _ctx.Languages.Select(
                    l => new SelectListItem
                    {
                        Text = l.Name,
                        Value = l.Id.ToString()
                    })
            };
            return View(sliderModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SliderModel sliderModel, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                AddModelValidNotifications();

                sliderModel.LanguageList = _ctx.Languages.Select(l => new SelectListItem { Text = l.Name, Value = l.Id.ToString() });

                return View(sliderModel);
            }

            try
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;

                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"img/slider/");
                var extension = Path.GetExtension(file.FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }

                sliderModel.ImagePath = @"img/slider/" + fileName + extension;

                var dbSlider = _mapper.Map<Slider>(sliderModel);

                _ctx.Sliders.Add(dbSlider);

                _ctx.SaveChanges();

                AddNotification("Kayıt başarıyla oluşturuldu!", Helpers.NotificationType.Success);                
            }
            catch
            {
                AddNotification("İşlem sırasında bir hata oluştu!", Helpers.NotificationType.Error);

                return View(sliderModel);
            }

            return RedirectToAction("Index");

        }

        public IActionResult Edit(int? id)
        {
            if(id==0 || id == null)
            {
                return NotFound();
            }

            var slider = _ctx.Sliders.FirstOrDefault(s => s.Id == id);

            if(slider == null)
            {
                return NotFound();
            }

            var sliderModel = _mapper.Map<SliderModel>(slider);

            sliderModel.LanguageList = _ctx.Languages.Select(l => new SelectListItem { Text = l.Name, Value = l.Id.ToString() });

            return View(sliderModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SliderModel sliderModel, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                AddModelValidNotifications();

                sliderModel.LanguageList = _ctx.Languages.Select(l => new SelectListItem { Text = l.Name, Value = l.Id.ToString() });

                return View(sliderModel);
            }

            try
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img/slider/");
                    var extension = Path.GetExtension(file.FileName);

                    var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, sliderModel.ImagePath.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    sliderModel.ImagePath = @"img/slider/" + fileName + extension;
                }

                var dbSlider = _mapper.Map<Slider>(sliderModel);

                _ctx.Sliders.Update(dbSlider);

                _ctx.SaveChanges();

                AddNotification("Kayıt başarıyla düzenlendi!", Helpers.NotificationType.Success);
            }
            catch
            {
                AddNotification("İşlem sırasında bir hata oluştu!", Helpers.NotificationType.Error);

                sliderModel.LanguageList = _ctx.Languages.Select(l => new SelectListItem { Text = l.Name, Value = l.Id.ToString() });

                return View(sliderModel);
            }
            
            return RedirectToAction("Index");

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var slider = _ctx.Sliders.FirstOrDefault(s => s.Id == id);

            if (slider == null)
            {
                return Json(new { success = false, message = "İşlem sırasında bir hata oluştu!" });
            }

            try
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, slider.ImagePath.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                _ctx.Sliders.Remove(slider);

                _ctx.SaveChanges();

                return Json(new { success = true, message = "Kayıt başarıyla silindi!" });
            }
            catch
            {
                return Json(new { success = true, message = "İşlem sırasında bir hata oluştu!" });
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sliderList = _ctx.Sliders.Include("Language").ToList();

            return Json(new { data = sliderList });
        }
    }
}
