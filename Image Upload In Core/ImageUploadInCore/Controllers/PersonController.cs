using ImageUploadInCore.Common.Abstraction;
using ImageUploadInCore.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadInCore.Controllers
{
    public class PersonController : Controller
    {
        public readonly IPersonService _person;
       
        private readonly IWebHostEnvironment _hostEnvironment;

        public PersonController(IPersonService person, IWebHostEnvironment hostEnvironment)
        {
            _person = person;
            _hostEnvironment = hostEnvironment;
        }

        // GET: PersonController
        public async Task<IActionResult> Index()
        {
            try
            {
                var person = await _person.GetAllPerson();
                return View(person);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: PersonController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var person = await _person.GetPersonById(id);
                if (person == null)
                    return NotFound();
                
                return View(person);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persons persons)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Save image to wwwroot/ image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(persons.ImageFile.FileName);
                    string extension = Path.GetExtension(persons.ImageFile.FileName);
                    persons.Photo = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await persons.ImageFile.CopyToAsync(fileStream);
                    }
                    var result = await _person.AddPerson(persons);
                    return RedirectToAction(nameof(Index));
                }
                return View(persons);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: PersonController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var result = await _person.GetPersonById(id);
            return View(result);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Persons persons)
        {
            try
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(persons.ImageFile.FileName);
                string extension = Path.GetExtension(persons.ImageFile.FileName);
                persons.Photo = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await persons.ImageFile.CopyToAsync(fileStream);
                }

                var dbStudent = await _person.GetPersonById(id);
                if (dbStudent == null)
                    return NotFound();
               await _person.UpdatePerson(persons, id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return StatusCode(HttpContext.Response.StatusCode, e.Message);
            }
        }

        // GET: PersonController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var img = await _person.GetPersonById(id);

            if (img == null)
            {
                return NotFound();
            }
            return View(img);
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                var person = await _person.GetPersonById(id);
                if (person == null)
                    return NotFound();
                await _person.DeletePerson(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
