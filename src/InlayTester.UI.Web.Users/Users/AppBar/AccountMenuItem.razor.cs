// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Users.AppBar;


public partial class AccountMenuItem
{
    [Inject]
    public required IUserSessionManager SessionManager { get; set; }


    private User? mUser;


    protected override void OnInitialized()
    {
        base.OnInitialized();

        SessionManager.CurrentChanged.Subscribe(UpdateUser);

        mUser = SessionManager.Current;
    }


    private void UpdateUser()
    {
        InvokeAsync(
            () => {
                mUser = SessionManager.Current;
                StateHasChanged();
            }
        );
    }

    private async Task OnLogin()
    {
        var user = await Shell.Dialogs.ShowLoginDialog(Loc["users.login.title-login"]);

        if (user != null)
        {
            SessionManager.Login(user);
        }
    }

    private async Task OnSwitch()
    {
        var user = await Shell.Dialogs.ShowLoginDialog(Loc["users.login.title-switch"]);

        if (user != null)
        {
            SessionManager.Login(user);
        }
    }

    private void OnLogout()
    {
        SessionManager.Logout();
    }
}
