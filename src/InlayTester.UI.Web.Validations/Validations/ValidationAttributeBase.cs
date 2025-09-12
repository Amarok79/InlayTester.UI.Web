// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.ComponentModel.DataAnnotations;
using InlayTester.Domain;


namespace InlayTester.UI.Web.Validations;


public abstract class ValidationAttributeBase : ValidationAttribute
{
    protected Localizable DefaultErrorMessage { get; init; }

    protected IStringLocalizer? Localizer { get; private set; }


    public override Boolean RequiresValidationContext => true;


    protected ValidationResult? Success()
    {
        return ValidationResult.Success;
    }

    protected ValidationResult Error(ValidationContext ctx)
    {
        return Error(DefaultErrorMessage, ctx);
    }

    protected ValidationResult Error(Localizable errorMessage, ValidationContext ctx)
    {
        return new ValidationResult(
            Localizer != null ? Localizer.GetText(errorMessage) : errorMessage.ResourceKey,
            [ ctx.MemberName! ]
        );
    }


    protected override ValidationResult? IsValid(Object? value, ValidationContext ctx)
    {
        Localizer = ctx.GetService<IStringLocalizer>();

        return ValidationResult.Success;
    }
}
