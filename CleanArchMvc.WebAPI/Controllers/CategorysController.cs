using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategorysController: Controller
    {
        private readonly ICategoryService _categoryService;

        public CategorysController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(Name = "Obter Categoria")]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            try
            {
                var categorys = await _categoryService.GetCategorys();
                return Ok(categorys);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao consultar" });
            }
        }

        [HttpGet("{id:int:min(1):maxlength(5)}")]
        public async Task<ActionResult<List<CategoryDTO>>> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao consultar" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Erro ao enviar");
            }
            try
            {
                await _categoryService.Add(category);
                return new CreatedAtRouteResult("Obter Categoria", new { category = category.Id }, category);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Erro ao enviar");
            }
        }

        [HttpPut("{id:int:min(1):maxlength(5)}")]
        public async Task<ActionResult<CategoryDTO>> Put(int? id, CategoryDTO category)
        {
            if (id is null || !ModelState.IsValid)
            {
                return BadRequest("Erro ao editar");
            }
            try
            {
                await _categoryService.Update(category);
                return Ok(category);
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao editar" });
            }
        }

        [HttpDelete("{id:int:min(1):maxlength(5)}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest("Erro ao remover");
            }
            try
            {
                await _categoryService.Remove((int)id);
                return Ok("Categoria removida");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao remover");
            }
        }
    }
}
