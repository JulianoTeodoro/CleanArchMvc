using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using Xunit.Sdk;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Product Object With Valid State")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Computador", "PC bom", 40, 104, "computer.png");
            action.Should().NotThrow<CleanArchMvc.Domain.Validations.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with Negative number ID")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Computador", "PC bom", 40, 104, "computer.png");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Invalid id negative");
        }

        [Fact(DisplayName = "Create Product with Short Name minimum 3 characters")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Ci", "PC bom", 40, 104, "computer.png");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Name too short, minimum 3 characters");
        }


        [Fact(DisplayName = "Nome vazio")]
        public void CreateProduct_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Product(1, "", "PC bom", 40, 104, "computer.png");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Nome vazio!");
        }

        [Fact(DisplayName = "Nome com valor nulo")]
        public void CreateProduct_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Product(1, null, "PC bom", 40, 104, "computer.png");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Nome com valor nulo!");
        }

        [Fact(DisplayName = "Imagem com valor muito grande")]
        public void CreateProduct_WithLongImageName_DomainExceptionInvalidImage()
        {
            Action action = () => new Product(1, "Computador", "PC bom", 40, 104, "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Imagem com valor muito grande!");
        }

        [Fact(DisplayName = "Imagem vazia")]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Computador", "PC bom", 40, 104, "");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Imagem vazia!");
        }

        [Fact(DisplayName = "Imagem nula")]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Computador", "PC bom", 40, 104, null);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Imagem nula!");
        }

        [Fact(DisplayName = "Imagem nula sem NullReference")]
        public void CreateProduct_WithNullImageName_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Computador", "PC bom", 40, 104, null);
            action.Should()
                .NotThrow<NullReferenceException>();
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Computador", "PC bom", value, 104, "Produto.png");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Estoque negativo!");
        }

        [Theory]
        [InlineData(-9)]
        public void CreateProduct_InvalidPriceValue_ExceptionDomainNegativeValue(decimal value)
        {
            Action action = () => new Product(1, "Computador", "PC bom", 50, value, "Produto.png");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Preço negativo!");
        }

    }
}
