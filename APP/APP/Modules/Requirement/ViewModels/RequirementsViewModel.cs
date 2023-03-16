namespace APP.ViewModels
{
    using APP.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class RequirementsViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<RequirementsItemViewModel> objRequirements;
        private String filter;
        #endregion

        #region Properties
        public ObservableCollection<RequirementsItemViewModel> ObjRequirements
        {
            get { return this.objRequirements; }
            set { SetValue(ref this.objRequirements, value); }
        }
        public Estandar<Requirement> Requirement
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
        public RequirementsViewModel()
        {
            LoadRequirements();
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
        public async void LoadRequirements()
        {
            this.IsRunning = true;
            Requirement = await MainViewModel.GetInstance().GetRequirements(0,10);
            this.IsRunning = false;
            if (Requirement != null)
            {
                this.ObjRequirements = new ObservableCollection<RequirementsItemViewModel>(
                 this.ToRequirementsItemViewModel());
            }
        }
        public async void LoadRequirementsSearch()
        {
            this.IsRunning = true;
            Requirement = await MainViewModel.GetInstance().GetRequirementsSearch(this.Filter, 0, 10);
            this.IsRunning = false;
            if (Requirement != null)
            {
                this.ObjRequirements = new ObservableCollection<RequirementsItemViewModel>(
            this.ToRequirementsItemViewModel());
            }
        }
        public void Search()
        {
            if (!string.IsNullOrEmpty(this.Filter))
            {
                this.LoadRequirementsSearch();
            }
            else
            {
                this.LoadRequirements();
            }
        }
        private IEnumerable<RequirementsItemViewModel> ToRequirementsItemViewModel()
        {
            return Requirement.lst.Select(l => new RequirementsItemViewModel
            {
                id = l.id,
                title = l.title,
                profile = l.profile,
                description = l.description,
                updatedAt = l.updatedAt,
                userUpdated = l.userUpdated,
                sessionToken = l.sessionToken,
                lstItem = l.lstItem,
                CONTENIDO_INTRO = MainViewModel.GetInstance().ValueContenidoIntro(l.description),
            });
        }
        
        #endregion
    }
}
