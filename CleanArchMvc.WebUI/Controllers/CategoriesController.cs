using CleanArchMvc.Application.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Application.DTOs;
using System.Security.Cryptography.X509Certificates;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategorys();
            return View(categories);
        }

        public IActionResult Create()
        {
            var category = new CategoryDTO();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }

            await _categoryService.Add(category);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("ID not provided");
            }

            var category = await _categoryService.GetById((int)id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDTO category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }

            await _categoryService.Update(category);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("ID not provided");
            }

            var category = await _categoryService.GetById((int)id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound("ID not provided");
            var category = await _categoryService.GetById((int)id);
            return View(category);
        }
    }
}
