using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using ReactiveUI;

namespace RxUi_Serilog.Helpers;

public class CustomObservableExceptionHandler : IObserver<Exception>
{
    public void OnNext(Exception value)
    {
        if (Debugger.IsAttached)
        {
            Debugger.Break();
        }

        try
        {
            Debug.WriteLine(value);
        }
        catch
        {
            // ignored
        }

        RxApp.MainThreadScheduler.Schedule(() =>
        {
            throw value;
        });
    }

    public void OnError(Exception error)
    {
        if (Debugger.IsAttached)
        {
            Debugger.Break();
        }

        try
        {
            Debug.WriteLine(error);
        }
        catch
        {
            // ignored
        }

        RxApp.MainThreadScheduler.Schedule(() =>
        {
            throw error;
        });
    }

    public void OnCompleted()
    {
        if (Debugger.IsAttached)
        {
            Debugger.Break();
        }

        RxApp.MainThreadScheduler.Schedule(() =>
        {
            throw new NotImplementedException();
        });
    }
}
