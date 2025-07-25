// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.UI.Web.Shell;
using Microsoft.Extensions.DependencyInjection;


namespace InlayTester.UI.Web;


public static class ShellServiceCollectionExtensions
{
    public static IServiceCollection AddShell(this IServiceCollection services)
    {
        services.AddScoped<IShell, ShellServiceImpl>();
        services.AddScoped<IShellDialogs>(x => x.GetRequiredService<IShell>().Dialogs);

        return services;
    }
}
