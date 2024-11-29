// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Services;


internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTomlStringLocalizer(this IServiceCollection services)
    {
        var translationDir = Path.Combine(AppContext.BaseDirectory, "translations");
        var localizer      = new BuiltInStringLocalizer(translationDir);

        services.AddSingleton<IStringLocalizer>(localizer);

        return services;
    }
}
