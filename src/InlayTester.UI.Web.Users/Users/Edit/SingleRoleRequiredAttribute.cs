// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.ComponentModel.DataAnnotations;
using InlayTester.UI.Web.Validations;


namespace InlayTester.UI.Web.Users.Edit;


public sealed class SingleRoleRequiredAttribute : ViewModelValidationAttributeBase<UserViewModel>
{
    public SingleRoleRequiredAttribute()
    {
        DefaultErrorMessage = "users.edit.validation-error-role-required";
    }


    protected override ValidationResult? IsValid(UserViewModel viewModel, ValidationContext ctx)
    {
        return viewModel.RolesCount == 0 ? Error(ctx) : Success();
    }
}
