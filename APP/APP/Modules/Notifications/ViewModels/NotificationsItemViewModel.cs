using APP.Models;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class NotificationsItemViewModel : Notification
    {
        public string CONTENIDO_INTRO { get; set; }

        public string ESTADO_COLOR
        {
            get;
            set;
        }

        #region Commands
        public ICommand SelectNotificationsItemCommand
        {
            get
            {
                return new Command(() => LoadNotificationsItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadNotificationsItem()
        {
            MainViewModel.GetInstance().NotificationsDetail = new NotificationsDetailViewModel(this);
            await App.Navigator.PushAsync(new NotificationsDetailPage());
        }
        #endregion
    }
}
