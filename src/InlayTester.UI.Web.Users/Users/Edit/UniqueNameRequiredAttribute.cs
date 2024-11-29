// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.ComponentModel.DataAnnotations;
using InlayTester.UI.Web.Validations;


namespace InlayTester.UI.Web.Users.Edit;


public sealed class UniqueNameRequiredAttribute : ViewModelValidationAttributeBase<UserViewModel>
{
    public UniqueNameRequiredAttribute()
    {
        DefaultErrorMessage = "users.edit.validation-error-name-exists";
    }


    protected override ValidationResult? IsValid(UserViewModel viewModel, ValidationContext ctx)
    {
        return viewModel.IsNameUnique switch {
            null  => Success(),
            true  => Success(),
            false => Error(ctx),
        };
    }
}
