using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CleanArchMvc.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Name = "Obter Produto")]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            try
            {
                var products = await _productService.GetProducts();
                return Ok(products);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao consultar" });
            }
        }

        [HttpGet("productCategory/{id:int:min(1):maxlength(5)}")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductCategory(int id)
        {
            try
            {
                var product = await _productService.GetProductCategory(id);
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao consultar" });
            }
        }

        [HttpGet("{id:int:min(1):maxlength(5)}")]
        public async Task<ActionResult<List<ProductDTO>>> GetById(int id)
        {
            try
            {
                var product = await _productService.GetById(id);
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao consultar" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Erro ao enviar");
            }
            try
            {
                await _productService.Add(product);
                return new CreatedAtRouteResult("Obter Produto", new { product = product.Id }, product);
            }
            catch(DbUpdateException)
            {
                return BadRequest("Erro ao enviar");
            }
        }

        [HttpPut("{id:int:min(1):maxlength(5)}")]
        public async Task<ActionResult<ProductDTO>> Put(int? id, ProductDTO product)
        {
            if(id is null || !ModelState.IsValid)
            {
                return BadRequest("Erro ao editar");
            }
            try
            {
                await _productService.Update(product);
                return Ok(product);
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
                await _productService.Remove((int)id);
                return Ok("Produto removido");
            }
            catch(Exception)
            {
                return BadRequest("Erro ao remover");
            }
        }
    }
}
