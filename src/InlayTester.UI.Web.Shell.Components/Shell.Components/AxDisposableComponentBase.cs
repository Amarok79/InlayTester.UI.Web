// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.Reactive.Disposables;


namespace InlayTester.UI.Web.Shell.Components;


public abstract class AxDisposableComponentBase : AxLoggingComponentBase,
    IDisposable,
    IAsyncDisposable
{
    private readonly CompositeDisposable mDisposables = new();


    public ICollection<IDisposable> Disposables => mDisposables;


    protected virtual void Dispose(Boolean disposing)
    {
        if (disposing)
        {
            mDisposables.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual ValueTask DisposeAsyncCore()
    {
        mDisposables.Dispose();

        return ValueTask.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }
}
