namespace APP.ViewModels
{
    using APP.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class NotificationsDetailViewModel : BaseViewModel
    {
        #region Attributes

        private Estandar<Notification> notification;
        #endregion

        #region Properties
        public Estandar<Notification> Notification
        {
            get { return this.notification; }
            set { SetValue(ref this.notification, value); }
        }
        public NotificationsItemViewModel NotificationsItem
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public NotificationsDetailViewModel(NotificationsItemViewModel notificationsItemViewModel)
        {
            NotificationsItem = notificationsItemViewModel;
            LoadNotifications();
        }
        #endregion

        #region Methods
        public async void LoadNotifications()
        {
            this.IsRunning = true;
            this.Notification = await MainViewModel.GetInstance().PostNotificationsById(NotificationsItem);
            this.IsRunning = false;
        }
        #endregion
    }
}
