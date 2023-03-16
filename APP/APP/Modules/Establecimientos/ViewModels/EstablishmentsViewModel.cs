namespace APP.ViewModels
{
    using APP.Models;
    using APP.Modules.Establecimientos.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EstablishmentsViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<EstablishmentsItemViewModel> objEstablishments;
        private ObservableCollection<string> listDepartamento;
        private ObservableCollection<string> listCiudad;
        private string selectedDepartamento;
        private string selectedCiudad;
        #endregion

        #region Properties
        public ObservableCollection<EstablishmentsItemViewModel> ObjEstablishments
        {
            get { return this.objEstablishments; }
            set { SetValue(ref this.objEstablishments, value); }
        }
        public ObservableCollection<string> ListDepartamento
        {
            get { return this.listDepartamento; }
            set { SetValue(ref this.listDepartamento, value); }
        }
        public ObservableCollection<string> ListCiudad
        {
            get { return this.listCiudad; }
            set { SetValue(ref this.listCiudad, value); }
        }
        public string SelectedDepartamento
        {
            get { return this.selectedDepartamento; }
            set { SetValue(ref this.selectedDepartamento, value);
                this.ObjEstablishments = new ObservableCollection<EstablishmentsItemViewModel>();
                this.ListCiudad = new ObservableCollection<string>();
                this.SelectedCiudad = string.Empty;
                LoadCiudades();
            }
        }
        public string SelectedCiudad
        {
            get { return this.selectedCiudad; }
            set { SetValue(ref this.selectedCiudad, value);
                if (!string.IsNullOrEmpty(value))
                {
                    LoadEstablishments();
                }
            }
        }
        public Estandar<Departamento> EstandarDepartamento
        {
            get;
            set;
        }
        public Estandar<Ciudad> EstandarCiudad
        {
            get;
            set;
        }
        public Estandar<Establishment> Establishment
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        public EstablishmentsViewModel()
        {
            LoadDepartamentos();
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() => LoadEstablishments());
            }
        }
        #endregion

        #region Methods
        public async void LoadDepartamentos()
        {
            this.IsRunning = true;
            this.ListDepartamento = new ObservableCollection<string>();
            EstandarDepartamento = await MainViewModel.GetInstance().GetDepartamentos();
            foreach (var departamento in EstandarDepartamento.lst)
            {
               this.ListDepartamento.Add(departamento.name);
            }
            this.IsRunning = false;
        }
        public async void LoadCiudades()
        {
            this.IsRunning = true;
            int departamentoId = this.EstandarDepartamento.lst.Where(x => x.name.Equals(this.SelectedDepartamento)).FirstOrDefault().id;
            this.ListCiudad = new ObservableCollection<string>();
            this.SelectedCiudad = string.Empty;
            EstandarCiudad = new Estandar<Ciudad>();
            EstandarCiudad = await MainViewModel.GetInstance().GetCiudades(departamentoId);
            foreach (var ciudad in EstandarCiudad.lst)
            {
                this.ListCiudad.Add(ciudad.name);
            }
            this.IsRunning = false;
        }
        public async void LoadEstablishments()
        {
            this.IsRunning = true;
            if (!string.IsNullOrEmpty(SelectedCiudad) && !string.IsNullOrEmpty(SelectedDepartamento))
            {
                int ciudadId = this.EstandarCiudad.lst.Where(x => x.name.Equals(this.SelectedCiudad) && x.regionName.Equals(this.SelectedDepartamento)).FirstOrDefault().id;
                Establishment = await MainViewModel.GetInstance().GetEstablishments(ciudadId, 0, 10);
                this.IsRunning = false;
                if (Establishment != null)
                {
                    this.ObjEstablishments = new ObservableCollection<EstablishmentsItemViewModel>(
                     this.ToEstablishmentsItemViewModel());
                }
            }
            this.IsRunning = false;
        }
        private IEnumerable<EstablishmentsItemViewModel> ToEstablishmentsItemViewModel()
        {
            return Establishment.lst.Select(l => new EstablishmentsItemViewModel
            {
                id = l.id,
                name = l.name,
                address = l.address,
                opertor = l.opertor,
                codContract = l.codContract,
                nuc = l.nuc,
                cityId = l.cityId,
                cityName = l.cityName,
                updatedAt = l.updatedAt,
                userUpdated = l.userUpdated,
                sessionToken = l.sessionToken,
                lstMachine = l.lstMachine,
            });
        }
        #endregion
    }
}
