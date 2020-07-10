// BackgroundTask.cs
//
// Author: Saimel Saez saimelsaez@gmail.com
//
// 7/9/2020
//
//

using System;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using Xam.Plugin.Backgrounding.Abstractions;

namespace Xam.Plugin.Backgrounding
{
    public class BackgroundTaskImplementation : BackgroundTask
    {
        private bool isRunning = false;

        public override void Start(Func<Task> action, CancellationTokenSource cts, Action<ProgressMessage> onProgressCallback = null)
        {
            if (isRunning == false)
            {
                isRunning = true;
                base.Start(action, cts, onProgressCallback);

                NSObject.InvokeInBackground(async () => await Task.Run(action, TokenSource.Token));
            }
        }

        public override void Stop()
        {
            if (isRunning == true)
            {
                isRunning = false;
                base.Stop();
            }
        }
    }
}
