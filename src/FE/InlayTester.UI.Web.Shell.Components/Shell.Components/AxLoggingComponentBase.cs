// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;


namespace InlayTester.UI.Web.Shell.Components;


public abstract class AxLoggingComponentBase : ComponentBase
{
    [Inject]
    public required ILoggerFactory LoggerFactory { get; set; }


    public ILogger Logger { get; set; } = NullLogger.Instance;


    protected override void OnInitialized()
    {
        base.OnInitialized();

        Logger = LoggerFactory.CreateLogger(GetType());
    }
}
