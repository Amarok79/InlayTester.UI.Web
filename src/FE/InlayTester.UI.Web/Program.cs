// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.Globalization;
using InlayTester.Infrastructure.Gateway;
using InlayTester.Infrastructure.Services.UserSession;
using InlayTester.UI.Web;
using InlayTester.UI.Web.Components;
using InlayTester.UI.Web.Services;
using InlayTester.UI.Web.Users;
using InlayTester.UI.Web.Users.List;
using Microsoft.AspNetCore.Localization;
using MudBlazor.Services;
using Serilog;
using Toolbelt.Blazor.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// infrastructure
builder.Services.AddLocalization();
builder.Services.AddMudServices();
builder.Services.AddHotKeys2();
builder.Services.AddTomlStringLocalizer();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddSerilog(
    (_, loggerConfiguration) => loggerConfiguration.MinimumLevel.Information()
        .Enrich.FromLogContext()
        .Enrich.WithThreadId()
        .WriteTo.Async(
            x => x.File(
                Path.Combine(
                    AppContext.BaseDirectory,
                    "..",
                    "logs",
                    "web",
                    "InlayTester.UI..log"
                ),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 31,
                rollOnFileSizeLimit: true,
                buffered: true,
                flushToDiskInterval: TimeSpan.FromSeconds(5),
                formatProvider: CultureInfo.InvariantCulture,
                outputTemplate:
                "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] [{ThreadId:000}] [{SourceContext}]  {Message:lj}{NewLine}{Exception}"
            ),
            4096,
            true
        )
        .WriteTo.Seq("http://192.168.178.44:5341")
);

// application
builder.Services.AddShell();
builder.Services.AddUsersServices();
builder.Services.AddGateway();
builder.Services.AddUserSessionManager();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseRequestLocalization(
    x => {
        x.ApplyCurrentCultureToResponseHeaders = true;
        x.DefaultRequestCulture                = new RequestCulture("en-US", "en");
    }
);

app.MapRazorComponents<App>().AddAdditionalAssemblies(typeof(UserList).Assembly).AddInteractiveServerRenderMode();

app.Run();
