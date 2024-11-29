// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.UI.Web.Shell;


namespace InlayTester.UI.Web.Users.Login;


internal sealed class ShellLoginDialogImpl : IShellLoginDialog
{
    private readonly IDialogService mDialogService;


    public ShellLoginDialogImpl(IDialogService dialogService)
    {
        mDialogService = dialogService;
    }


    public async Task<User?> ShowLoginDialog(
        String title,
        String? acceptLabel = null,
        String? cancelLabel = null
    )
    {
        var param = new DialogParameters<LoginDialog>();
        param.Add(x => x.Title, title);
        param.Add(x => x.AcceptButton, acceptLabel);
        param.Add(x => x.CancelButton, cancelLabel);

        var options = new DialogOptions {
            BackdropClick    = false,
            CloseOnEscapeKey = true,
            MaxWidth         = MaxWidth.Medium,
        };

        var dialog = await mDialogService.ShowAsync<LoginDialog>(title, param, options);

        return await dialog.GetReturnValueAsync<User?>();
    }
}
