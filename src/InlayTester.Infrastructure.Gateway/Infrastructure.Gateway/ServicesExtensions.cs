// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.Domain;
using Microsoft.Extensions.DependencyInjection;


namespace InlayTester.Infrastructure.Gateway;


public static class ServicesExtensions
{
    public static void AddGateway(this IServiceCollection services)
    {
        services.AddSingleton<GrpcChannelProvider>();
        services.AddSingleton<IUserManager, GrpcUserManager>();
    }
}
