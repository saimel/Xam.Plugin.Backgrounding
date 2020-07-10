// App.xaml.cs
//
// Author: Saimel Saez saimelsaez@gmail.com
//
// 7/9/2020
//
//

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Backgrounding.Demo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
