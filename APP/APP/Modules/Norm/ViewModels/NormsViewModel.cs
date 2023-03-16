namespace APP.ViewModels
{
    using APP.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class NormsViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<NormsItemViewModel> objNorms;
        private String filter;
        #endregion

        #region Properties
        public ObservableCollection<NormsItemViewModel> ObjNorms
        {
            get { return this.objNorms; }
            set { SetValue(ref this.objNorms, value); }
        }
        public Estandar<Norm> Norm
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
        public NormsViewModel()
        {
            LoadNorms();
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
        public async void LoadNorms()
        {
            this.IsRunning = true;
            Norm = await MainViewModel.GetInstance().GetNorms();
            this.IsRunning = false;
            if (Norm != null)
            {
                this.ObjNorms = new ObservableCollection<NormsItemViewModel>(
                this.ToNormsItemViewModel());
            }
        }
        public async void LoadNormsSearch()
        {
            this.IsRunning = true;
            Norm = await MainViewModel.GetInstance().PostNormsSearch(this.Filter, 0, 10);
            this.IsRunning = false;
            if (Norm != null)
            {
                this.ObjNorms = new ObservableCollection<NormsItemViewModel>(
                this.ToNormsItemViewModel());
            }
        }
        public void Search()
        {
            if (!string.IsNullOrEmpty(this.Filter))
            {
                this.LoadNormsSearch();
            }
            else
            {
                this.LoadNorms();
            }
        }
        private IEnumerable<NormsItemViewModel> ToNormsItemViewModel()
        {
            return Norm.lst.Select(l => new NormsItemViewModel
            {
                id = l.id,
                title = l.title,
                description = l.description,
                publish = l.publish,
                updatedAt = l.updatedAt,
                userUpdated = l.userUpdated,
                sessionToken = l.sessionToken,
                CONTENIDO_INTRO = MainViewModel.GetInstance().ValueContenidoIntro(l.description),
            });
        }
        #endregion
    }
}
