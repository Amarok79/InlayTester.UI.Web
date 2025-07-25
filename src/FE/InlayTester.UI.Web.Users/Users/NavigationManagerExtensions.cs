// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Users;


public static class NavigationManagerExtensions
{
    public static void GotoHome(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo("/");
    }

    public static void GotoUsers(this NavigationManager navigationManager)
    {
        navigationManager.NavigateTo("/users");
    }

    public static void GotoUsers(this NavigationManager navigationManager, Id id)
    {
        navigationManager.NavigateTo($"/users/{id}");
    }
}
