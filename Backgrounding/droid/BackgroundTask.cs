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
using Android.Content;
using Xam.Plugin.Backgrounding.Abstractions;

namespace Xam.Plugin.Backgrounding
{
    public class BackgroundTaskImplementation : BackgroundTask
    {
        private Intent intent;
        private Context context;
        private BackgroundingReceiver receiver;

        public override void Init(Context context)
        {
            this.context = context;
        }

        public override void Start(Func<Task> action, CancellationTokenSource cts, Action<ProgressMessage> onProgressCallback = null)
        {
            if (intent == null)
            {
                base.Start(action, cts, onProgressCallback);

                receiver = new BackgroundingReceiver(action, TokenSource.Token);
                intent = new Intent(context, typeof(BackgroundingService));

                context.RegisterReceiver(receiver, new IntentFilter(Constants.IntentStartKey));

                context.StartService(intent);
            }
        }

        public override void Stop()
        {
            if (intent != null)
            {
                base.Stop();

                context.StopService(intent);
                context.UnregisterReceiver(receiver);

                receiver = null;
                intent = null;
            }
        }
    }
}
