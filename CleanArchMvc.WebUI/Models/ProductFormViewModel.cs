using CleanArchMvc.Application.DTOs;
using CleanArchMvc.WebUI.Controllers;

namespace CleanArchMvc.WebUI.Models
{
    public class ProductFormViewModel
    {
        public ProductDTO Product { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}
