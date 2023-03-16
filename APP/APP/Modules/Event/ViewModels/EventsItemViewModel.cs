using APP.Models;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class EventsItemViewModel : Event
    {
        public string CONTENIDO_INTRO { get; set; }

        #region Commands
        public ICommand SelectEventsItemCommand
        {
            get
            {
                return new Command(() => LoadEventsItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadEventsItem()
        {
            MainViewModel.GetInstance().EventsDetail = new EventsDetailViewModel(this);
            await App.Navigator.PushAsync(new EventsDetailPage());
        }
        #endregion
    }
}
