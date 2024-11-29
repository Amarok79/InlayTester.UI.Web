// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.UI.Web.Shell;
using InlayTester.UI.Web.Users.Login;
using Microsoft.Extensions.DependencyInjection;


namespace InlayTester.UI.Web.Users;


public static class UserServiceCollectionExtensions
{
    public static IServiceCollection AddUsersServices(this IServiceCollection services)
    {
        services.AddScoped<IShellLoginDialog, ShellLoginDialogImpl>();

        return services;
    }
}
