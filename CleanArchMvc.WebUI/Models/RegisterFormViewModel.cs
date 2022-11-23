using CleanArchMvc.Infra.Data.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.WebUI.Models
{
    public class RegisterFormViewModel
    {
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
