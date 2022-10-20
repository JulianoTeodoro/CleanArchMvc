using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class CategoryUnitTest1
    {

        [Fact(DisplayName = "Create Category Object With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Computador");
            action.Should().NotThrow<CleanArchMvc.Domain.Validations.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category with Negative number ID")]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Celular");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Invalid id negative");
        }

        [Fact(DisplayName = "Create Category with Short Name minimum 3 characters")]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Name too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Nome vazio")]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(3, "");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Nome vazio!");
        }

        [Fact(DisplayName = "Nome com valor nulo")]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(2, null);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validations.DomainExceptionValidation>()
                    .WithMessage("Nome vazio!");
        }

    }
}