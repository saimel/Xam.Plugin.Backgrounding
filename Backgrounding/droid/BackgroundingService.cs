// BackgroundingService.cs
//
// Author: Saimel Saez saimelsaez@gmail.com
//
// 7/9/2020
//
//

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace Xam.Plugin.Backgrounding
{
    [Service]
    public class BackgroundingService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Application.Context.SendBroadcast(new Intent(Constants.IntentStartKey));

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
