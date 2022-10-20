using CleanArchMvc.Domain.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Stock { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product(string name, string descricao, int stock, decimal preco, string image)
        {
            ValidateDomain(name, descricao, stock, preco, image);
        }

        public Product(int id, string Name, string Description, int Stock, decimal Price, string Image)
        {
            DomainExceptionValidation.When(id < 0, "Invalid value id");
            Id = id;
            ValidateDomain(Name, Description, Stock, Price, Image);
        }

        public void Update(string Name, string Description, int Stock, decimal Price, string Image, int categoryId)
        {
            ValidateDomain(Name, Description, Stock, Price, Image);
            CategoryId = categoryId;
        }

        private void ValidateDomain(string Name, string Description, int Stock, decimal Price, string Image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(Name), "Invalid name null");
            DomainExceptionValidation.When(Name.Length < 3, "Invalid name menor que 3");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Description), "Invalid descricao null");
            DomainExceptionValidation.When(Description.Length < 5, "Invalid descricao menor que 3");
            DomainExceptionValidation.When(Stock < 0, "Invalid stock negativo");
            DomainExceptionValidation.When(Price < 0, "Invalid preço negativo");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Image), "Invalid image null");
            DomainExceptionValidation.When(Image?.Length > 250, "Invalid image maior que 250");

            this.Name = Name;
            this.Description = Description;
            this.Stock = Stock;
            this.Price = Price;
            this.Image = Image;

        }
    }
}