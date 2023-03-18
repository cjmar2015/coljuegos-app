using APP.Models;
using APP.Modules.Establecimientos.Models;
using APP.Services;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class MasterDashboardViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService; 
        #endregion

        #region Attributes
        private ObservableCollection<MasterModuleItemViewModel> modules;
        public List<MasterModuleItemViewModel> ListaModules
        {
            get;
            set;
        }
        #endregion

        #region Properties
        public ObservableCollection<MasterModuleItemViewModel> Modules
        {
            get { return this.modules; }
            set { SetValue(ref this.modules, value); }
        }
        #endregion

        #region Constructors
        public MasterDashboardViewModel()
        {
            LoadModules();
        }
        #endregion

        #region Commands

        public ICommand ShowCuentaCommand
        {
            get
            {
                return new Command(() => ShowCuenta());
            }
        }
        public ICommand ShowEventsCommand
        {
            get
            {
                return new Command(() => ShowEvents());
            }
        }
        public ICommand ShowNormsCommand
        {
            get
            {
                return new Command(() => ShowNorms());
            }
        }
        public ICommand ShowFaqCommand
        {
            get
            {
                return new Command(() => ShowFaq());
            }
        }
        public ICommand ShowPqrsCommand
        {
            get
            {
                return new Command(() => ShowPQRS());
            }
        }
        public ICommand ShowRequirementsCommand
        {
            get
            {
                return new Command(() => ShowRequirements());
            }
        }
        public ICommand ShowEstablishmentsCommand
        {
            get
            {
                return new Command(() => ShowEstablishments());
            }
        }
        public ICommand ShowChannelCommand
        {
            get
            {
                return new Command(() => ShowChannel());
            }
        }
        public ICommand ShowNotificationsCommand
        {
            get
            {
                return new Command(() => ShowNotifications());
            }
        }
        #endregion

        #region Methods
        public void LoadModules()
        {
            MenuModules();
            Modules = new ObservableCollection<MasterModuleItemViewModel>(
                this.ToModuleItemViewModel());
        }
        public void MenuModules()
        {
            this.ListaModules = new List<MasterModuleItemViewModel>();

            this.ListaModules.Add(new MasterModuleItemViewModel()
            {
                PägeName = "Events",
                Title = "Eventos",
                Imagen = "ic_eventos.png",
                BackgroundColor = "WhiteSmoke",
                TextColor = "#000000",
                BorderColor = "#3F51B5",
            });
            this.ListaModules.Add(new MasterModuleItemViewModel()
            {
                PägeName = "Establishment",
                Title = "Establishment",
                Imagen = "ic_establecimiento.png",
                BackgroundColor = "WhiteSmoke",
                TextColor = "#000000",
                BorderColor = "#3F51B5",
            });
            this.ListaModules.Add(new MasterModuleItemViewModel()
            {
                PägeName = "Channel",
                Title = "Channel",
                Imagen = "ic_canales.png",
                BackgroundColor = "WhiteSmoke",
                TextColor = "#000000",
                BorderColor = "#3F51B5",
            });
            this.ListaModules.Add(new MasterModuleItemViewModel()
            {
                PägeName = "Requirement",
                Title = "Requirement",
                Imagen = "ic_requerimientos.png",
                BackgroundColor = "WhiteSmoke",
                TextColor = "#000000",
                BorderColor = "#3F51B5",
            });
            this.ListaModules.Add(new MasterModuleItemViewModel()
            {
                PägeName = "Norm",
                Title = "Norm",
                Imagen = "ic_normas.png",
                BackgroundColor = "WhiteSmoke",
                TextColor = "#000000",
                BorderColor = "#3F51B5",
            });
            this.ListaModules.Add(new MasterModuleItemViewModel()
            {
                PägeName = "Faq",
                Title = "Faq",
                Imagen = "ic_faq.png",
                BackgroundColor = "WhiteSmoke",
                TextColor = "#000000",
                BorderColor = "#3F51B5",
            });
            this.ListaModules.Add(new MasterModuleItemViewModel()
            {
                PägeName = "PQRS",
                Title = "PQRS",
                Imagen = "ic_pqrs.png",
                BackgroundColor = "WhiteSmoke",
                TextColor = "#000000",
                BorderColor = "#3F51B5",
            });
        }
        private IEnumerable<MasterModuleItemViewModel> ToModuleItemViewModel()
        {
            return ListaModules.Select(l => new MasterModuleItemViewModel
            {
                PägeName = l.PägeName,
                Title = l.Title,
                Mensaje = l.Mensaje,
                Valor = l.Valor,
                BackgroundColor = l.BackgroundColor,
                TextColor = l.TextColor,
                BorderColor = l.BorderColor,
                Imagen = l.Imagen
            });
        }
        private async void ShowCuenta()
        {
            if (MainViewModel.GetInstance().Sesion == null)
            {
                MainViewModel.GetInstance().MasterLogin = new MasterLoginViewModel();
                await App.Navigator.PushAsync(new MasterLoginPage());
            }
            else
            {
                await App.Navigator.PushAsync(new MasterSesionPage());
            }
        }
        private async void ShowEvents()
        {
            MainViewModel.GetInstance().Events = new EventsViewModel();
            await App.Navigator.PushAsync(new EventsPage());
        }
        private async void ShowFaq()
        {
            MainViewModel.GetInstance().Faqs = new FaqsViewModel();
            await App.Navigator.PushAsync(new FaqsPage());
        }
        private async void ShowNorms()
        {
            MainViewModel.GetInstance().Norms = new NormsViewModel();
            await App.Navigator.PushAsync(new NormsPage());
        }
        private async void ShowEstablishments()
        {
            this.IsRunning = true;
            Estandar<EstablishmentUrl> establishmentUrl = await MainViewModel.GetInstance().GetParamUrlEstablishment();
            this.IsRunning = false;
            if (establishmentUrl.obj != null)
            {
                MainViewModel.GetInstance().MasterWeb = new MasterWebViewModel(establishmentUrl.obj.valueVar);
                await App.Navigator.PushAsync(new EstablishmentsWebPage());
            }

            //MainViewModel.GetInstance().Establishments = new EstablishmentsViewModel();
            //await App.Navigator.PushAsync(new EstablishmentsPage());
        }
        private async void ShowPQRS()
        {
            this.IsRunning = true;
            Estandar<PQRSUrl> pQRSUrl = await MainViewModel.GetInstance().GetParamUrlPQRS();
            this.IsRunning = false;
            if (pQRSUrl.obj != null)
            {
                MainViewModel.GetInstance().MasterWeb = new MasterWebViewModel(pQRSUrl.obj.valueVar);
                await App.Navigator.PushAsync(new PqrsWebPage());
            }

            //MainViewModel.GetInstance().Establishments = new EstablishmentsViewModel();
            //await App.Navigator.PushAsync(new EstablishmentsPage());
        }
        private async void ShowChannel()
        {
            MainViewModel.GetInstance().MasterWeb = new MasterWebViewModel("https://www.coljuegos.gov.co/publicaciones/306312/canales-de-atencion/");
            await App.Navigator.PushAsync(new ChannelWebPage());
        }
        private async void ShowRequirements()
        {
            MainViewModel.GetInstance().Requirements = new RequirementsViewModel();
            await App.Navigator.PushAsync(new RequirementsPage());
        }
        private async void ShowNotifications()
        {
            if (MainViewModel.GetInstance().Sesion == null)
            {
                MainViewModel.GetInstance().MasterLogin = new MasterLoginViewModel();
                await App.Navigator.PushAsync(new MasterLoginPage());
            }
            else
            {
                MainViewModel.GetInstance().Notifications = new NotificationsViewModel();
                await App.Navigator.PushAsync(new NotificationsPage());
            }
        }
        #endregion
    }
}
