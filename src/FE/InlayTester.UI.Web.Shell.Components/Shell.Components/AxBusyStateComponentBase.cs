// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Shell.Components;


public abstract class AxBusyStateComponentBase : AxDisposableComponentBase
{
    public Boolean IsBusy { get; set; }


    protected override void OnInitialized()
    {
        base.OnInitialized();

        IsBusy = true;
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

    protected virtual Task OnLoading()
    {
        return Task.CompletedTask;
    }
}
