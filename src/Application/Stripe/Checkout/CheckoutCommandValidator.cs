using FluentValidation;

namespace Application.Stripe.Checkout;
internal sealed class CheckoutCommandValidator : AbstractValidator<CheckoutCommand>
{
    public CheckoutCommandValidator()
    {
        RuleFor(c => c.Currency).NotEmpty();
        RuleFor(c => c.Items).NotEmpty();
    }
}
