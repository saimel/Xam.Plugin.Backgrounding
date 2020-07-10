// CrossBackgroundTask.cs
//
// Author: Saimel Saez saimelsaez@gmail.com
//
// 7/7/2020
//
//

using System;
using System.Threading;

namespace Xam.Plugin.Backgrounding
{
    public static class CrossBackgroundTask
    {
        private static Lazy<IBackgroundTask> implementation = new Lazy<IBackgroundTask>(() => CreateBackgroundImplementation(), LazyThreadSafetyMode.PublicationOnly);

        public static IBackgroundTask Current
        {
            get
            {
                IBackgroundTask ret = implementation.Value;
                if(ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }

                return ret;
            }
        }

        private static IBackgroundTask CreateBackgroundImplementation()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#else
            return new BackgroundTaskImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
           new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
}
