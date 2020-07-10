// IOSBackgroundTask.cs
//
// Author: Saimel Saez saimelsaez@gmail.com
//
// 7/9/2020
//
//

using System;
using System.Threading.Tasks;
using Foundation;

namespace Xam.Plugin.Backgrounding
{
    public class IOSBackgroundTask
    {
        public void Start(Func<Task> action)
        {
            NSObject.InvokeInBackground(new Action(async () => await action()));
        }
    }
}
