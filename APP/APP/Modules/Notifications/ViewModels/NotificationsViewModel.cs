namespace APP.ViewModels
{
    using APP.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class NotificationsViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<NotificationsItemViewModel> objNotificationsUnread;
        private ObservableCollection<NotificationsItemViewModel> objNotifications;
        private String filter;
        #endregion

        #region Properties
        public ObservableCollection<NotificationsItemViewModel> ObjNotificationsUnread
        {
            get { return this.objNotificationsUnread; }
            set { SetValue(ref this.objNotificationsUnread, value); }
        }
        public ObservableCollection<NotificationsItemViewModel> ObjNotifications
        {
            get { return this.objNotifications; }
            set { SetValue(ref this.objNotifications, value); }
        }
        public Estandar<Notification> NotificationAll
        {
            get;
            set;
        }
        public Estandar<Notification> NotificationUnread
        {
            get;
            set;
        }
        public List<DTONotification> DTONotification
        {
            get;
            set;
        }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
            }
        }
        #endregion

        #region Constructors
        public NotificationsViewModel()
        {
            LoadNotifications();
        }
        #endregion

        #region Commands

        #endregion

        #region Methods
        public async void LoadNotifications()
        {
            this.IsRunning = true;
            NotificationUnread = await MainViewModel.GetInstance().GetNotificationsUnRead(MainViewModel.GetInstance().Sesion.username, 0, 10);
            NotificationAll = await MainViewModel.GetInstance().GetNotificationsAll(MainViewModel.GetInstance().Sesion.username,0,10);
            DTONotification = new List<DTONotification>();
            this.IsRunning = false;
            ObjNotifications = new ObservableCollection<NotificationsItemViewModel>();
            if (NotificationUnread != null)
            {
                DTONotification = DTONotification.Union(ToDTONotificationsItemViewModel(NotificationUnread, "#004478")).ToList();
                //foreach (var item in DTONotification)
                //{
                //    MainViewModel.GetInstance().SendNotification(item.subject, item.message);
                //}
            }
            
            if (NotificationAll != null)
            {
                DTONotification = DTONotification.Union(ToDTONotificationsItemViewModel(NotificationAll, "LightGray")).ToList();
                
            }
            ObjNotifications = new ObservableCollection<NotificationsItemViewModel>(
                this.ToNotificationsItemViewModel(DTONotification));
        }
        private IEnumerable<NotificationsItemViewModel> ToNotificationsItemViewModel(List<DTONotification> dTONotification)
        {
            return dTONotification.Select(l => new NotificationsItemViewModel
            {
                id = l.id,
                subject = l.subject,
                message = l.message,
                channel = l.channel,
                userDstId = l.userDstId,
                userDstUsername = l.userDstUsername,
                createdAt = l.createdAt,
                readAt = l.readAt,
                lstUserDstUsername = l.lstUserDstUsername,
                ESTADO_COLOR = l.color,
                CONTENIDO_INTRO = MainViewModel.GetInstance().ValueContenidoIntro(l.message),
            });
        }
        private List<DTONotification> ToDTONotificationsItemViewModel(Estandar<Notification> notifications, string color)
        {
            return notifications.lst.Select(l => new DTONotification
            {
                id = l.id,
                subject = l.subject,
                message = l.message,
                channel = l.channel,
                userDstId = l.userDstId,
                userDstUsername = l.userDstUsername,
                createdAt = l.createdAt,
                readAt = l.readAt,
                lstUserDstUsername = l.lstUserDstUsername,
                color = color
            }).ToList();
        }
        #endregion
    }
}
