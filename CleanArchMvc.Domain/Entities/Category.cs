using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Validations;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid id menor que 0");
            Id = id;
            ValidateDomain(name);
        }

        public void Update(string nome)
        {
            ValidateDomain(nome);
        }
        public ICollection<Product> Products { get; set; }

        private void ValidateDomain(string nome)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Invalid name null");
            DomainExceptionValidation.When(nome.Length < 3, "Invalid name menor que 3");

            Name = nome;

        }
    }
}
