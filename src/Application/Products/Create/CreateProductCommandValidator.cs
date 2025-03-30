using FluentValidation;

namespace Application.Products.Create;
internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Images).NotEmpty();
        RuleFor(c => c.Price).NotNull().GreaterThan(0);
        RuleFor(c => c.Currency).NotEmpty();
    }
}
