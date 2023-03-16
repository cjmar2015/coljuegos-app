using APP.Helpers;
using APP.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APP
{
    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterDetailPage Master { get; internal set; }
        public static string Data { get; set; }
        #endregion

        public App() 
        {
            InitializeComponent();
            //if (Settings.IsLogin)
            //{
                MainPage = new MasterPage();
            //}
            //else
            //{
                //MainPage = new MasterLoginPage();
            //}
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
