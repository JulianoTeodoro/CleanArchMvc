using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {

        [MinLength(2, ErrorMessage = "Minimo de 2 caracteres")]
        [StringLength(80, ErrorMessage = "Maximo de 80 caracteres")]
        public string? Nome { get; set; }

        public string? SobreNome { get; set; }

    }
}
