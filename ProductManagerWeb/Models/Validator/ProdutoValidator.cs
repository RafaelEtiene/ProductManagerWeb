using FluentValidation;
using ProductManager.Models.Entities;

namespace ProductManager.Models.Validator
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres");

            RuleFor(p => p.Preco)
                .NotEmpty().WithMessage("O preço é obrigatório")
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero");
        }
    }
}