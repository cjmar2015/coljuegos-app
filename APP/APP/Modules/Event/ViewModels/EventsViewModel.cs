namespace APP.ViewModels
{
    using APP.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EventsViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<EventsItemViewModel> objEvents;
        private String filter;
        #endregion

        #region Properties
        public ObservableCollection<EventsItemViewModel> ObjEvents
        {
            get { return this.objEvents; }
            set { SetValue(ref this.objEvents, value); }
        }
        public Estandar<Event> Evento
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
        public EventsViewModel()
        {
            LoadEvents();
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new Command(() => Search());
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() => Search());
            }
        }
        #endregion

        #region Methods
        public async void LoadEvents()
        {
            this.IsRunning = true;
            Evento = await MainViewModel.GetInstance().GetEvents();
            this.IsRunning = false;
            if (Evento != null)
            {
                this.ObjEvents = new ObservableCollection<EventsItemViewModel>(
                this.ToEventsItemViewModel());
            }
        }
        public async void LoadEventsSearch()
        {
            this.IsRunning = true;
            Evento = await MainViewModel.GetInstance().PostEventsSearch(this.Filter, 0, 10);
            this.IsRunning = false;
            if (Evento != null)
            {
                this.ObjEvents = new ObservableCollection<EventsItemViewModel>(
                this.ToEventsItemViewModel());
            }
        }
        public void Search()
        {
            if (!string.IsNullOrEmpty(this.Filter))
            {
                this.LoadEventsSearch();
            }
            else
            {
                this.LoadEvents();
            }
        }
        private IEnumerable<EventsItemViewModel> ToEventsItemViewModel()
        {
            return Evento.lst.Select(l => new EventsItemViewModel
            {
                id = l.id,
                title = l.title,
                description = l.description,
                eventAt= l.eventAt,
                publish = l.publish,
                publishAt = l.publishAt,
                updatedAt = l.updatedAt,
                userUpdated = l.userUpdated,
                sessionToken = l.sessionToken,
                CONTENIDO_INTRO = MainViewModel.GetInstance().ValueContenidoIntro(l.description),
            });
        }
        #endregion
    }
}
