// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.ComponentModel.DataAnnotations;
using InlayTester.UI.Web.Validations;


namespace InlayTester.UI.Web.Users.Edit;


public sealed class MatchingPasswordsRequiredAttribute : ViewModelValidationAttributeBase<UserViewModel>
{
    public MatchingPasswordsRequiredAttribute()
    {
        DefaultErrorMessage = "users.edit.validation-error-password-mismatch";
    }


    protected override ValidationResult? IsValid(UserViewModel viewModel, ValidationContext ctx)
    {
        if (!viewModel.IsFormValidation)
        {
            return Success();
        }

        if (viewModel.Password1.IsNullOrWhitespace() || viewModel.Password2.IsNullOrWhitespace())
        {
            return Success();
        }

        return String.Equals(viewModel.Password1, viewModel.Password2, StringComparison.Ordinal)
            ? Success()
            : Error(ctx);
    }
}
