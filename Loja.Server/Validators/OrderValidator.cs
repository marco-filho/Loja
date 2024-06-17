using Loja.Server.Dtos;
using FluentValidation;

namespace Loja.Server.Validators
{
    public class OrderValidator : BaseValidator<OrderDto>
    {
        public OrderValidator()
        {
            RuleFor(vc => vc.ClientName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome do cliente é obrigatório")
                .MinimumLength(2)
                .MaximumLength(100)
                .WithMessage("Nome do cliente deve ter entre 2 e 100 caracteres");

            RuleFor(vc => vc.OrderedAt)
                .NotNull()
                .NotEmpty()
                .WithMessage("Data do pedido é obrigatória")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Data do pedido deve ser anterior ou igual à data atual");

            RuleForEach(x => x.Items)
                .ChildRules(product =>
                {
                    product
                        .RuleFor(x => x.Amount)
                        .NotNull()
                        .NotEmpty()
                        .GreaterThan(0)
                        .WithMessage("A quantidade do produto deve ser um valor positivo");

                    product
                        .RuleFor(x => x.Price)
                        .NotNull()
                        .NotEmpty()
                        .GreaterThan(0)
                        .WithMessage("O preço do produto deve ser um valor positivo");
                });
        }
    }
}