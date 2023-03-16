namespace APP.ViewModels
{
    using APP.Models;
    using APP.Modules.Establecimientos.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class EstablishmentsDetailViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<MachinesItemViewModel> machines;
        #endregion

        #region Properties
        public ObservableCollection<MachinesItemViewModel> Machines
        {
            get { return this.machines; }
            set { SetValue(ref this.machines, value); }
        }

        public EstablishmentsItemViewModel EstablishmentsItem
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public EstablishmentsDetailViewModel(EstablishmentsItemViewModel establishmentsItemViewModel)
        {
            EstablishmentsItem = establishmentsItemViewModel;
            LoadEstablishments();
        }
        #endregion

        #region Methods
        public async void LoadEstablishments()
        {
            this.IsRunning = true;

            this.IsRunning = false;
            this.Machines = new ObservableCollection<MachinesItemViewModel>(
             this.ToMachinesItemViewModel());
        }
        private IEnumerable<MachinesItemViewModel> ToMachinesItemViewModel()
        {
            return EstablishmentsItem.lstMachine.Select(l => new MachinesItemViewModel
            {
                id = l.id,
                cod = l.cod,
                brand = l.brand,
                codBet = l.codBet,
                isMetOnline = l.isMetOnline,
                updatedAt = l.updatedAt,
                userUpdated = l.userUpdated
            });
        }
        #endregion
    }
}
