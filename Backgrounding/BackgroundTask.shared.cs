// BackgroundTask.shared.cs
//
// Author: Saimel Saez saimelsaez@gmail.com
//
// 7/10/2020
//
//

using System;
using System.Threading;
using System.Threading.Tasks;
using Xam.Plugin.Backgrounding.Abstractions;
using Xamarin.Forms;

namespace Xam.Plugin.Backgrounding
{
    public abstract class BackgroundTask : IBackgroundTask
    {
        public CancellationTokenSource TokenSource { get; set; }

        public virtual void Start(Func<Task> action, CancellationTokenSource cts, Action<ProgressMessage> onProgressCallback = null)
        {
            TokenSource = cts;

            MessagingCenter.Subscribe<ProgressMessage>(this, nameof(ProgressMessage), (obj) =>
            {
                Device.BeginInvokeOnMainThread(() => onProgressCallback?.Invoke(obj));
            });
        }

        public void SendProgress(object data)
        {
            var progressMessage = new ProgressMessage { Data = data };
            MessagingCenter.Send(progressMessage, nameof(ProgressMessage));
        }

        public virtual void Stop()
        {
            TokenSource?.Cancel();
            MessagingCenter.Unsubscribe<ProgressMessage>(this, nameof(ProgressMessage));
        }

#if MONOANDROID

        public abstract void Init(Android.Content.Context context);

#endif
    }
}
