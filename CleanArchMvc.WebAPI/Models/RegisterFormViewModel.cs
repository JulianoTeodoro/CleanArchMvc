using CleanArchMvc.Infra.Data.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.WebAPI.Models
{
    public class RegisterFormViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Minimo de 2 caracteres")]
        [StringLength(80, ErrorMessage = "Maximo de 80 caracteres")]
        public string Nome { get; set; }

        [Required]
        public string SobreNome { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Data format error")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Minimo de 10 e maximo de 20 caracteres", MinimumLength = 10)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senhas diferentes")]
        public string ConfirmPassword { get; set; }
    }
}
