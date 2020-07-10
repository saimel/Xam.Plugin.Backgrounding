// IBackgroundTask.shared.cs
//
// Author: Saimel Saez saimelsaez@gmail.com
//
// 7/9/2020
//
//

using System;
using System.Threading;
using System.Threading.Tasks;
using Xam.Plugin.Backgrounding.Abstractions;

namespace Xam.Plugin.Backgrounding
{
    public interface IBackgroundTask
    {
        CancellationTokenSource TokenSource { get; set; }

        void Start(Func<Task> action, CancellationTokenSource cts, Action<ProgressMessage> onProgressCallback = null);

        void SendProgress(object data);

        void Stop();

#if MONOANDROID

        void Init(Android.Content.Context context);

#endif
    }
}
