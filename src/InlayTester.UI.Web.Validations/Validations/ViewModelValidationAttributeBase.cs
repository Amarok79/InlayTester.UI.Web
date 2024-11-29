// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.ComponentModel.DataAnnotations;


namespace InlayTester.UI.Web.Validations;


public abstract class ViewModelValidationAttributeBase<TViewModel> : ValidationAttributeBase
{
    protected sealed override ValidationResult? IsValid(Object? value, ValidationContext ctx)
    {
        base.IsValid(value, ctx);

        if (ctx.ObjectInstance is TViewModel vm)
        {
            return IsValid(vm, ctx);
        }

        return Success();
    }

    protected virtual ValidationResult? IsValid(TViewModel model, ValidationContext ctx)
    {
        return Success();
    }
}
