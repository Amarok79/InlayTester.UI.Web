// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.Domain;


namespace InlayTester.UI.Web.Shell;


internal sealed class ShellServiceImpl : IShell,
    IShellDialogs
{
    private readonly IDialogService mDialogService;
    private readonly IShellLoginDialog mLoginDialog;

    private readonly DialogOptions mOptions = new() {
        BackdropClick    = false,
        CloseOnEscapeKey = true,
        MaxWidth         = MaxWidth.Medium,
    };


    public IShellDialogs Dialogs => this;


    public ShellServiceImpl(IDialogService dialogService, IShellLoginDialog loginDialog)
    {
        mDialogService = dialogService;
        mLoginDialog   = loginDialog;
    }


    public async Task ShowErrorDialog(String message, String? title = null, String? errorCode = null)
    {
        var param = new DialogParameters<ErrorDialog>();
        param.Add(x => x.Title, title);
        param.Add(x => x.ErrorCode, errorCode);
        param.Add(x => x.Message, message);

        var dialog = await mDialogService.ShowAsync<ErrorDialog>(title, param, mOptions);

        await dialog.GetReturnValueAsync<Object>();
    }

    public async Task<Boolean> ShowDeleteConfirmationDialog(
        String message,
        String title,
        String? acceptLabel = null,
        String? cancelLabel = null
    )
    {
        var param = new DialogParameters<QuestionDialog>();
        param.Add(x => x.Title, title);
        param.Add(x => x.Message, message);
        param.Add(x => x.AcceptButton, acceptLabel);
        param.Add(x => x.CancelButton, cancelLabel);

        var dialog = await mDialogService.ShowAsync<QuestionDialog>(title, param, mOptions);

        return await dialog.GetReturnValueAsync<Boolean?>() ?? false;
    }

    public Task<User?> ShowLoginDialog(String title, String? acceptLabel = null, String? cancelLabel = null)
    {
        return mLoginDialog.ShowLoginDialog(title, acceptLabel, cancelLabel);
    }
}
