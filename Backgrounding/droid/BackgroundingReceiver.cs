// BackgroundingReceiver.cs
//
// Author: Saimel Saez saimelsaez@gmail.com
//
// 7/9/2020
//
//

using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;

namespace Xam.Plugin.Backgrounding
{
    [IntentFilter(new[] { Constants.IntentStartKey })]
    internal class BackgroundingReceiver : BroadcastReceiver
    {
        private CancellationToken _token;
        private readonly Func<Task> _backgroundTask;

        public BackgroundingReceiver(Func<Task> backgroundTask, CancellationToken token)
        {
            _token = token;
            _backgroundTask = backgroundTask;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            new Action(async () => await Task.Run(_backgroundTask, _token)).Invoke();
        }
    }
}
