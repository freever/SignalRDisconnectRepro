using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SignalRDisconnectRepro.Droid
{
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        private HubConnection Connection;

        public MainApplication(IntPtr handle, JniHandleOwnership transer)
            :base(handle, transer)
        {
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;  
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
        }
        
        public override void OnCreate()
        {
            
            Connection = new HubConnectionBuilder()
                .WithUrl("https://s1.blauhaustechnology.com:5201/")
                .ConfigureLogging(x => x.SetMinimumLevel(LogLevel.Debug))
                .AddMessagePackProtocol()
                .Build();


            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            CrossCurrentActivity.Current.Init(this);

        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public async void OnActivityPaused(Activity activity)
        {
            await Connection.StopAsync();
        }

        public async void OnActivityResumed(Activity activity)
        {
            await Connection.StartAsync();
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}