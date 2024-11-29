// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Shell;


public interface IShellDialogs : IShellLoginDialog
{
    Task ShowErrorDialog(String message, String? title = null, String? errorCode = null);

    Task<Boolean> ShowDeleteConfirmationDialog(
        String message,
        String title,
        String? acceptLabel = null,
        String? cancelLabel = null
    );
}
