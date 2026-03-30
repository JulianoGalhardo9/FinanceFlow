using FluentValidation;

namespace FinanceFlow.Application.Commands.AddAsset.Validators;

public class AddAssetValidator : AbstractValidator<AddAssetCommand>
{
    public AddAssetValidator() 
    {
        RuleFor(x => x.Ticker)
            .NotEmpty().WithMessage("O código do ativo (Ticker) é obrigatório.")
            .Length(4, 6).WithMessage("O Ticker deve ter entre 4 e 6 caracteres.")
            .Matches(@"^[A-Z]{4}[0-9]{1,2}$").WithMessage("Formato de Ticker inválido (Ex: PETR4, BOVA11).");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

        RuleFor(x => x.PurchasePrice)
            .GreaterThan(0).WithMessage("O preço de compra deve ser maior que zero.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("A moeda é obrigatória.")
            .Must(c => c == "BRL" || c == "USD").WithMessage("No momento, aceitamos apenas BRL ou USD.");
    }
}