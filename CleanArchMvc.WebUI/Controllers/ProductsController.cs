using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categorys = await _categoryService.GetCategorys();
            var viewmodel = new ProductFormViewModel { Categories = categorys };
            return View(viewmodel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if(!ModelState.IsValid)
            {
                var categorys = await _categoryService.GetCategorys();
                var viewmodel = new ProductFormViewModel { Product = product, Categories = categorys };
                return View(viewmodel);
            }
            await _productService.Add(product);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("Id not provided");
            }
            var categorys = await _categoryService.GetCategorys();
            var product = await _productService.GetProductCategory((int)id);
            var viewmodel = new ProductFormViewModel { Product = product, Categories = categorys };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            if(!ModelState.IsValid)
            {
                var categorys = await _categoryService.GetCategorys();
                var viewmodel = new ProductFormViewModel { Product = product, Categories = categorys };
                return View(viewmodel);
            }

            await _productService.Update(product);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound("Id not provided");

            var product = await _productService.GetProductCategory((int)id);
            if (product is null) return NotFound("Produto não encontrado");

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductCategory(id);
            return View(product);
        }


    }
}
