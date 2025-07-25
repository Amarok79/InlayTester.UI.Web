// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.Domain;


namespace InlayTester.UI.Web.Shell;


public interface IShellLoginDialog
{
    Task<User?> ShowLoginDialog(String title, String? acceptLabel = null, String? cancelLabel = null);
}
