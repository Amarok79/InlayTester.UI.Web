// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.ComponentModel.DataAnnotations;
using InlayTester.UI.Web.Validations;


namespace InlayTester.UI.Web.Users.Login;


public sealed class MatchingPasswordRequiredAttribute : ViewModelValidationAttributeBase<LoginViewModel>
{
    public MatchingPasswordRequiredAttribute()
    {
        DefaultErrorMessage = "users.login.validation-error-password-wrong";
    }


    protected override ValidationResult? IsValid(LoginViewModel viewModel, ValidationContext ctx)
    {
        if (!viewModel.IsFormValidation)
        {
            return Success();
        }

        if (viewModel.SelectedUser == null || viewModel.Password.IsNullOrWhitespace())
        {
            return Success();
        }

        return String.Equals(viewModel.Password, viewModel.SelectedUser.Password, StringComparison.Ordinal)
            ? Success()
            : Error(ctx);
    }
}
