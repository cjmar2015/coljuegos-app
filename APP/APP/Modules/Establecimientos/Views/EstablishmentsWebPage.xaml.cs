using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentsWebPage : ContentPage
    {
        /// Use this to wait on the page to be finished with/closed/dismissed
        public Task PageClosedTask { get { return tcs.Task; } }

        private TaskCompletionSource<bool> tcs { get; set; }

        public EstablishmentsWebPage()
        {
            InitializeComponent();
            tcs = new System.Threading.Tasks.TaskCompletionSource<bool>();
        }
        // Either override OnDisappearing 
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            tcs.SetResult(true);
        }
    }
}