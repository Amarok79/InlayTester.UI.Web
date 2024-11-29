// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.UI.Web.Validations;


namespace InlayTester.UI.Web.Users.Login;


public sealed class LoginViewModel
{
    public Int32 MaxPasswordLength => User.MaxPasswordLength;


    public Boolean IsFormValidation { get; set; }


    public List<User> Users { get; set; } = [ ];

    [FieldRequired]
    public User? SelectedUser { get; set; }

    [FieldRequired, MatchingPasswordRequired]
    public String Password { get; set; } = String.Empty;
}
