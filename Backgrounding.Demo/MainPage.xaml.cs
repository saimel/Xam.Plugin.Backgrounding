// MainPage.xaml.cs
//
// Author: Saimel Saez saimelsaez@gmail.com
//
// 7/9/2020
//
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xam.Plugin.Backgrounding;
using Xam.Plugin.Backgrounding.Abstractions;
using Xamarin.Forms;

namespace Backgrounding.Demo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public ICommand StartCommand { get; set; }

        public ICommand StopCommand { get; set; }

        public string Counter
        {
            get => (string)GetValue(CounterProperty);
            set => SetValue(CounterProperty, value);
        }

        public static BindableProperty CounterProperty = BindableProperty.Create(
            nameof(Counter), typeof(string), typeof(MainPage), default(string));

        public MainPage()
        {
            StartCommand = BuildStartCommand();
            StopCommand = BuildStopCommand();

            InitializeComponent();
            BindingContext = this;
        }

        private ICommand BuildStartCommand()
        {
            return new Command(() =>
            {
                cts = new CancellationTokenSource();
                CrossBackgroundTask.Current.Start(DummyTask, cts, pm => Counter = pm.Data.ToString());
            });
        }

        private CancellationTokenSource cts;

        private ICommand BuildStopCommand()
        {
            return new Command(() =>
            {
                CrossBackgroundTask.Current.Stop();
            });
        }

        private async Task DummyTask()
        {
            try
            {
                for (int i = 0; i < 100; ++i)
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
    }
}
