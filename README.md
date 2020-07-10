# Xam.Plugin.Backgrounding

Cross platform API for running tasks in the background.

### Supported platforms

| Name | Tested |
| - | - |
| Android | Yes |
| iOS | Yes |

### NuGet Information

Package is available on [Xam.Plugin.Backgrounding]()

## Setup

* Install int PCL/.NetStandard project and client projects.
* Follow additional setup instructions for each platform.

### iOS Setup

* No additional steps required.

### Android Setup

Add this line inside your `MainActivity.OnCreate()` after Xamarin Forms initialization:

```C#
 CrossBackgroundTask.Current.Init(this);
```            

## How to use it

To start running a task in the background, all you need to do is to execute `CrossBackgroundTask.Current.Start(your-params)` with required parameters. To stop a task, just execute `CrossBackgroundTask.Current.Stop()`. Also, you may need to send some progress information about your task, all you have to do is execute `CrossBackgroundTask.Current.SendProgress(your-data)`. Let's see an example:


```C#
private CancellationTokenSource cts;

private void SendToBackground() 
{
	cts = new CancellationTokenSource();
	CrossBackgroundTask.Current.Start(DummyTask, cts, pm => Counter = pm.Data.ToString());
}

private void StopRunningTask()
{
	CrossBackgroundTask.Current.Stop();
}

private async Task DummyTask()
{
    try
    {
        for (int i = 0; i < 3599; ++i)
        {
            await Task.Delay(1000, cts.Token);

            string data = $"Running for {(i / 60).ToString().PadLeft(2, '0')}:{(i % 60).ToString().PadLeft(2, '0')}";
            CrossBackgroundTask.Current.SendProgress(data);
        }
    }
    catch (OperationCanceledException ex)
    {
        System.Diagnostics.Debug.WriteLine(ex.Message);
    }
}
```

#### Important

* You must provide an instance of `CancellationTokenSource` to `Start()`.
* Execute your awaitable task inside a `try/catch(OperationCanceledException)`. Otherwise you will get an unhandled exeption when the task gets cancelled.
* Make sure to provide the instance of `CancellarionTokenSource` to all awaitable tasks. This way all running tasks will be cancelled.
* `CrossBackgroundTask` can not run several tasks in the background.
* If you provided an `onProgressCallback` action to `Start()` method, then `CrossBackgroundTask.Current.SendProgress()` executes this callback in the main thread. This will allow you to update your UI in a safe way.
* Check out the full sample code if required.