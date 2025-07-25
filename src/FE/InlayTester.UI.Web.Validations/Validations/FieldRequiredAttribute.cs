// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.ComponentModel.DataAnnotations;


namespace InlayTester.UI.Web.Validations;


public sealed class FieldRequiredAttribute : ValidationAttributeBase
{
    public FieldRequiredAttribute()
    {
        DefaultErrorMessage = "shell.validations.validation-error-required";
    }


    protected override ValidationResult? IsValid(Object? value, ValidationContext ctx)
    {
        base.IsValid(value, ctx);

        return value switch {
            null                                       => Error(ctx),
            String x when String.IsNullOrWhiteSpace(x) => Error(ctx),
            _                                          => Success(),
        };
    }
}
