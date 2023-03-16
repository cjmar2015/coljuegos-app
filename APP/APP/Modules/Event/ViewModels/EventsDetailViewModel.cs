namespace APP.ViewModels
{
    using APP.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class EventsDetailViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<EventLstFileItemViewModel> eventLstFiles;
        private Estandar<Event> evento;
        #endregion

        #region Properties
        public ObservableCollection<EventLstFileItemViewModel> EventLstFiles
        {
            get { return this.eventLstFiles; }
            set { SetValue(ref this.eventLstFiles, value); }
        }
        public Estandar<Event> Evento
        {
            get { return this.evento; }
            set { SetValue(ref this.evento, value); }
        }

        public EventsItemViewModel EventsItem
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public EventsDetailViewModel(EventsItemViewModel eventsItemViewModel)
        {
            EventsItem = eventsItemViewModel;
            LoadEvents();
        }
        #endregion

        #region Methods
        public async void LoadEvents()
        {
            this.IsRunning = true;
            this.Evento = await MainViewModel.GetInstance().PostEventsById(EventsItem);
            this.IsRunning = false;
            this.EventLstFiles = new ObservableCollection<EventLstFileItemViewModel>(
             this.ToEventLstFileItemViewModel());
        }
        private IEnumerable<EventLstFileItemViewModel> ToEventLstFileItemViewModel()
        {
            return Evento.obj.lstFiles.Select(l => new EventLstFileItemViewModel
            {
                id = l.id,
                url = l.url,
                nameFile = l.nameFile,
                contentB64 = l.contentB64,
                type = l.type,
                description = l.description,
                updatedAt = l.updatedAt,
                userUpdated = l.userUpdated,
                sessionToken = l.sessionToken
            });
        }
        #endregion
    }
}
