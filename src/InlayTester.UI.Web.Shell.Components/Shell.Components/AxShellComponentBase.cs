// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using Microsoft.Extensions.Logging;
using Toolbelt.Blazor.HotKeys2;


namespace InlayTester.UI.Web.Shell.Components;


public abstract class AxShellComponentBase : AxBusyStateComponentBase
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required HotKeys HotKeys { get; set; }

    [Inject]
    public required IShell Shell { get; set; }

    [Inject]
    public required IStringLocalizer Loc { get; set; }


    private HotKeysContext? mHotKeysContext;


    protected override void OnAfterRender(Boolean firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            mHotKeysContext = HotKeys.CreateContext();

            OnInitializeHotKeys(mHotKeysContext);
        }
    }

    protected virtual void OnInitializeHotKeys(HotKeysContext context)
    {
    }


    protected override async ValueTask DisposeAsyncCore()
    {
        if (mHotKeysContext != null)
        {
            await mHotKeysContext.DisposeAsync();
        }

        await base.DisposeAsyncCore();
    }


    protected Task<Boolean> RunAsync(String errorCode, Func<Task> asyncAction)
    {
        return RunAsync(errorCode, asyncAction, e => Task.CompletedTask);
    }

    protected Task<Boolean> RunAsync(String errorCode, Func<Task> asyncAction, Action errorAction)
    {
        return RunAsync(
            errorCode,
            asyncAction,
            e => {
                errorAction.Invoke();
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
                errorAction?.Invoke(e);
                return Task.CompletedTask;
            }
        );
    }

    protected Task<Boolean> RunAsync(String errorCode, Func<Task> asyncAction, Func<Task> asyncErrorAction)
    {
        return RunAsync(errorCode, asyncAction, e => asyncErrorAction.Invoke());
    }

    protected async Task<Boolean> RunAsync(
        String errorCode,
        Func<Task> asyncAction,
        Func<Exception, Task>? asyncErrorAction
    )
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

            if (asyncErrorAction != null)
            {
                await asyncErrorAction(e);
            }

            IsBusy = false;
            StateHasChanged();

            return false;
        }
    }
}
