// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.Domain;
using Microsoft.Extensions.DependencyInjection;


namespace InlayTester.Infrastructure.Services.UserSession;


public static class ServicesExtensions
{
    public static void AddUserSessionManager(this IServiceCollection services)
    {
        services.AddSingleton<IUserSessionManager, UserSessionManager>();
    }
}
