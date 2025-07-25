// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Users.Login;


public partial class LoginDialog
{
    [Inject]
    public required IUserManager UserManager { get; set; }

    [Inject]
    public required IUserSessionManager SessionManager { get; set; }


    [CascadingParameter]
    private IMudDialogInstance? Dialog { get; set; }

    [Parameter]
    public required String Title { get; set; }

    [Parameter]
    public required String? AcceptButton { get; set; }

    [Parameter]
    public required String? CancelButton { get; set; }


    private readonly LoginViewModel mViewModel;
    private readonly EditContext mEditContext;


    public LoginDialog()
    {
        mViewModel   = new LoginViewModel();
        mEditContext = new EditContext(mViewModel);
    }


    protected override async Task OnLoading()
    {
        await base.OnLoading();

        await RunAsync(
            "USR120",
            async () => {
                var users = await UserManager.QueryUsersAsync();

                mViewModel.Users        = users.OrderBy(x => x.Name).ToList();
                mViewModel.SelectedUser = SessionManager.Current;
            },
            () => Dialog?.Close(DialogResult.Cancel())
        );
    }

    private async Task OnAccept()
    {
        try
        {
            mViewModel.IsFormValidation = true;

            if (mViewModel.SelectedUser != null)
            {
                if (!await RunAsync(
                        "USR121",
                        async () => {
                            mViewModel.SelectedUser = await UserManager.GetUserAsync(mViewModel.SelectedUser.Id);
                        }
                    ))
                {
                    return;
                }
            }

            if (mEditContext.Validate())
            {
                Dialog?.Close(DialogResult.Ok(mViewModel.SelectedUser));
            }
        }
        finally
        {
            mViewModel.IsFormValidation = false;
        }
    }

    private void OnCancel()
    {
        Dialog?.Close(DialogResult.Cancel());
    }
}
