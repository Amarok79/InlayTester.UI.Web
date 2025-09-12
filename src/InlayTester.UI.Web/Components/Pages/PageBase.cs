// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.UI.Web.Shell;
using Microsoft.Extensions.Logging.Abstractions;
using Toolbelt.Blazor.HotKeys2;


namespace InlayTester.UI.Web.Components.Pages;


public abstract class PageBase : ComponentBase,
    IAsyncDisposable
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IShell Shell { get; set; }

    [Inject]
    public required IShellDialogs ShellDialogs { get; set; }

    [Inject]
    public required IDialogService DialogService { get; set; }

    [Inject]
    public required HotKeys HotKeys { get; set; }

    [Inject]
    public required IStringLocalizer Loc { get; set; }

    [Inject]
    public required ILoggerFactory LoggerFactory { get; set; }

    public ILogger Logger { get; set; } = NullLogger.Instance;

    public Boolean IsBusy { get; set; }


    private HotKeysContext? mHotKeysContext;


    protected override void OnInitialized()
    {
        base.OnInitialized();

        Logger = LoggerFactory.CreateLogger(GetType());

        IsBusy = true;
    }

    protected override void OnAfterRender(Boolean firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            mHotKeysContext = HotKeys.CreateContext();

            OnInitializeHotKeys(mHotKeysContext);
        }
    }

    protected override async Task OnAfterRenderAsync(Boolean firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await OnLoading();

            IsBusy = false;
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (mHotKeysContext != null)
        {
            await mHotKeysContext.DisposeAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }


    protected virtual void OnInitializeHotKeys(HotKeysContext context)
    {
    }

    protected virtual Task OnLoading()
    {
        return Task.CompletedTask;
    }


    protected Task<Boolean> RunAsync(String errorCode, Func<Task> asyncAction, Action? errorAction = null)
    {
        return RunAsync(
            errorCode,
            asyncAction,
            e => {
                errorAction?.Invoke();
                return Task.CompletedTask;
            }
        );
    }

    protected Task<Boolean> RunAsync(String errorCode, Func<Task> asyncAction, Action<Exception> errorAction)
    {
        return RunAsync(
            errorCode,
            asyncAction,
            e => {
                errorAction(e);
                return Task.CompletedTask;
            }
        );
    }

    protected Task<Boolean> RunAsync(String errorCode, Func<Task> asyncAction, Func<Task> errorAction)
    {
        return RunAsync(errorCode, asyncAction, e => errorAction());
    }

    protected async Task<Boolean> RunAsync(String errorCode, Func<Task> asyncAction, Func<Exception, Task> errorAction)
    {
        try
        {
            IsBusy = true;
            StateHasChanged();

            await asyncAction();

            IsBusy = false;
            StateHasChanged();

            return true;
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error with error code '{ErrorCode}' occurred", errorCode);

            await Shell.Dialogs.ShowErrorDialog(e.GetBaseException().Message, errorCode: errorCode);

            await errorAction(e);

            IsBusy = false;
            StateHasChanged();

            return false;
        }
    }
}
