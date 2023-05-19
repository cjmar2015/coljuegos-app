namespace APP.ViewModels
{
    using APP.Helpers;
    using APP.Models;
    using APP.ViewModels;
    using APP.Services;
    //using APP.Views;

    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using APP.Modules.Establecimientos.Models;
    using Plugin.LocalNotification;

    public class MainViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;

        #endregion

        #region Atributes
        private readonly string uRLAPI = "https://coljuegos.co:443";
        private Image imageUsuario;
        private string empresa = "COLJUEGOS";
        private string currentVersion;
        private string token;
        private string tokenType;
        #endregion

        #region Properties
        private ApiService ApiService
        {
            get { return this.apiService; }
            set { SetValue(ref this.apiService, value); }
        }
        public string URLAPI
        {
            get { return this.uRLAPI; }
        }
        public string Empresa
        {
            get { return this.empresa; }
            set { SetValue(ref this.empresa, value); }
        }
        public string CurrentVersion
        {
            get { return this.currentVersion; }
            set { SetValue(ref this.currentVersion, value); }
        }
        public string Token
        {
            get { return this.token; }
            set { SetValue(ref this.token, value); }
        }
        public string TokenType
        {
            get { return this.tokenType; }
            set { SetValue(ref this.tokenType, value); }
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public Image ImageUsuario
        {
            get { return this.imageUsuario; }
            set { SetValue(ref this.imageUsuario, value); }
        }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            CurrentVersion = VersionTracking.CurrentVersion;
            ApiService = new ApiService();
            
            this.MasterMenu = new ObservableCollection<MasterMenuItemViewModel>();
            this.LoadMasterMenu();
            this.LoadImageDefault();

            //Usuario = new Usuario();

            MasterInformation = new MasterInformationViewModel();
            //PersonaMyAccount = new PersonaMyAccountViewModel();
            MasterInformation = new MasterInformationViewModel();

            //if (Settings.IsLogin)
            //{
                Dashboards = new MasterDashboardViewModel();
                //_ = LoadPersonaReload();
            //}
            //else
            //{
            //    this.MasterLogin = new MasterLoginViewModel();
            //}
        }
        #endregion

        #region Methods
        public async Task<bool> LoadPersonaReload()
        {
            LoadMasterMenu();
            SyncNotifications();
            return true;
        }
        public void LoadImageDefault()
        {
            this.ImageUsuario = new Image
            {
                Source = "worker.png"
            };
        }
        public string ValueContenidoIntro(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length > 120)
                {
                    return value.Substring(0, 120);
                }
            }
            return value;
        }
        private IEnumerable<MasterMenuItemViewModel> ToMenuItemViewModel()
        {
            return ListMasterMenu.Select(l => new MasterMenuItemViewModel
            {
                Icon = l.Icon,
                PägeName = l.PägeName,
                Title = l.Title
            });
        }
        #endregion

        #region Functions
        public void SendNotification(String title, String message)
        {
            Random myObject = new Random();
            int ranNum = myObject.Next(1, 1000);
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Title = title,
                Description = message,
                ReturningData = "LocalNotification",
                NotificationId = ranNum,
            };
            LocalNotificationCenter.Current.Show(notification);
        }
        #endregion

        #region Modules

        #region Module - Master
        private ObservableCollection<MasterMenuItemViewModel> masterMenu;

        #region Properties
        public ObservableCollection<MasterMenuItemViewModel> MasterMenu
        {
            get { return this.masterMenu; }
            set { SetValue(ref this.masterMenu, value); }
        }
        public List<MasterMenuItemViewModel> ListMasterMenu
        {
            get;
            set;
        }
        public MasterInformationViewModel MasterInformation
        {
            get;
            set;
        }
        public MasterWebViewModel MasterWeb
        {
            get;
            set;
        }
        public MasterLoginViewModel MasterLogin
        {
            get;
            set;
        }
        public MasterDashboardViewModel Dashboards
        {
            get;
            set;
        }
        #endregion


        #region Methods
        public void LoadMasterMenu()
        {
            this.ListMasterMenu = new List<MasterMenuItemViewModel>();

            this.ListMasterMenu.Add(new MasterMenuItemViewModel
            {
                Icon = "ic_info",
                PägeName = "Información",
                Title = "Información"
            });
            if (Sesion == null)
            {
                this.ListMasterMenu.Add(new MasterMenuItemViewModel
                {
                    Icon = "ic_login",
                    PägeName = "LoginPage",
                    Title = "Ingresar"
                });
            }
            if (Sesion != null)
            {
                this.ListMasterMenu.Add(new MasterMenuItemViewModel
                {
                    Icon = "ic_login",
                    PägeName = "Requirement",
                    Title = "Requisitos"
                });
                this.ListMasterMenu.Add(new MasterMenuItemViewModel
                {
                    Icon = "ic_login",
                    PägeName = "ClosePage",
                    Title = "Cerrar sesión"
                });
            }
            this.MasterMenu = new ObservableCollection<MasterMenuItemViewModel>(
                this.ToMenuItemViewModel());
        }
        #endregion
        #endregion

        #region Module - RotatorSlider

        #region Properties
        public List<RotatorSlider> ListApiRotatorSlider
        {
            get;
            set;
        }
        #endregion

        #region DB
        //private async Task<List<RotatorSlider>> GetRotatorSliderFromAPI()
        //{
        //    ListApiRotatorSlider = new List<RotatorSlider>();
        //    Response response = await apiService.GetList<RotatorSlider>(
        //        Settings.UrlAPI + "/api/RotatorSliders/ByProyecto/");
        //    if (response.IsSuccess)
        //    {
        //        ListApiRotatorSlider = (List<RotatorSlider>)response.Result;
        //    }
        //    return ListApiRotatorSlider;
        //}
        #endregion

        #endregion

        #region Module - Usuarios

        #region Atributes
        private Sesion sesion;
        #endregion

        #region Properties
        public Sesion Sesion
        {
            get { return this.sesion; }
            set { SetValue(ref this.sesion, value); }
        }
        #endregion

        #region Funciones
        public async Task<Sesion> PostLogin(Login login)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Sesion> response = new Response<Sesion>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Sesion, Login>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0001-security/login/login", login);
            }
            if (response.body == null)
            {
                return null;
            }
            return (Sesion)response.body;
        }
        #endregion

        #endregion

        #region Module - Cuentas
        #region Properties
        public NewAccountViewModel NewAccount
        {
            get;
            set;
        }
        public ChangePasswordViewModel ChangePasswordAccount
        {
            get;
            set;
        }
        public RememberAccountViewModel RememberAccount
        {
            get;
            set;
        }
        #endregion

        #region Functions
        public async Task<Estandar<User>> PostNewAccount(User user)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<User>> response = new Response<Estandar<User>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<User>, User>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0001-security/manage-user/create-user", user);
            }
            return (Estandar<User>)response.body;
        }
        public async Task<Estandar<UserChange>> PostChangePassword(UserChange userChange)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<UserChange>> response = new Response<Estandar<UserChange>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<UserChange>, UserChange>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0001-security/manage-user/change-password-tmp", userChange);
            }
            return (Estandar<UserChange>)response.body;
        }
        #endregion
        #region Functions
        public async Task<Estandar<User>> PostRememberPassword(User user)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<User>> response = new Response<Estandar<User>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<User>, User>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0001-security/manage-user/remember-password", user);
            }
            return (Estandar<User>)response.body;
        }
        #endregion
        #endregion

        #region Module - Eventos

        #region Properties
        public EventsViewModel Events
        {
            get;
            set;
        }
        public EventsDetailViewModel EventsDetail
        {
            get;
            set;
        }
        #endregion

        #region Funciones
        public async Task<Estandar<Event>> GetEvents()
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Event>> response = new Response<Estandar<Event>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Event>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-event/list-published-active?numRecordsByPag=10&currentPag=0");
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Event>)response.body;
        }
        public async Task<Estandar<Event>> PostEventsSearch(string value, int currentPag, int numRecordsByPag)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Event>> response = new Response<Estandar<Event>>();
            EstandarPostSearch eventPostSearch = new EstandarPostSearch()
            {
                query = value,
                currentPag = currentPag,
                numRecordsByPag = numRecordsByPag
            };
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<Event>, EstandarPostSearch>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-event/search-published-active", eventPostSearch);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Event>)response.body;
        }
        public async Task<Estandar<Event>> PostEventsById(EventsItemViewModel eventsItemViewModel)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Event>> response = new Response<Estandar<Event>>();
            EstandarPost eventPost = new EstandarPost()
            {
                id = eventsItemViewModel.id,
                //sessionToken = Settings.Token
            };
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<Event>, EstandarPost>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-event/detail", eventPost);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Event>)response.body;
        }
        #endregion
        #endregion

        #region Module - Faq

        #region Properties
        public FaqsViewModel Faqs
        {
            get;
            set;
        }
        public FaqsDetailViewModel FaqsDetail
        {
            get;
            set;
        }
        #endregion

        #region Functions
        public async Task<Estandar<Faq>> GetFaqs()
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Faq>> response = new Response<Estandar<Faq>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Faq>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-faq/list-published?numRecordsByPag=10&currentPag=0");
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Faq>)response.body;
        }
        public async Task<Estandar<Faq>> PostFaqsSearch(string value, int currentPag, int numRecordsByPag)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Faq>> response = new Response<Estandar<Faq>>();
            EstandarPostSearch faqPostSearch = new EstandarPostSearch()
            {
                query = value,
                currentPag = currentPag,
                numRecordsByPag = numRecordsByPag
            };
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<Faq>, EstandarPostSearch>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-faq/search-published", faqPostSearch);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Faq>)response.body;
        }
        public async Task<Estandar<Faq>> PostFaqsById(FaqsItemViewModel faqsItemViewModel)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Faq>> response = new Response<Estandar<Faq>>();
            EstandarPost faqPost = new EstandarPost()
            {
                id = faqsItemViewModel.id,
                //sessionToken = Settings.Token
            };
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<Faq>, EstandarPost>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-faq/detail", faqPost);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Faq>)response.body;
        }

        #endregion

        #endregion

        #region Module - Norm

        #region Properties
        public NormsViewModel Norms
        {
            get;
            set;
        }
        public NormsDetailViewModel NormsDetail
        {
            get;
            set;
        }
        #endregion

        #region Functions
        public async Task<Estandar<Norm>> GetNorms()
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Norm>> response = new Response<Estandar<Norm>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Norm>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-norm/list-published?numRecordsByPag=10&currentPag=0");
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Norm>)response.body;
        }
        public async Task<Estandar<Norm>> PostNormsSearch(string value, int currentPag, int numRecordsByPag)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Norm>> response = new Response<Estandar<Norm>>();
            EstandarPostSearch normPostSearch = new EstandarPostSearch()
            {
                query = value,
                currentPag = currentPag,
                numRecordsByPag = numRecordsByPag
            };
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<Norm>, EstandarPostSearch>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-norm/search-published", normPostSearch);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Norm>)response.body;
        }
        public async Task<Estandar<Norm>> PostNormsById(NormsItemViewModel normsItemViewModel)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Norm>> response = new Response<Estandar<Norm>>();
            EstandarPost normPost = new EstandarPost()
            {
                id = normsItemViewModel.id,
                //sessionToken = Settings.Token
            };
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<Norm>, EstandarPost>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-norm/detail", normPost);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Norm>)response.body;
        }
        #endregion

        #endregion

        #region Module - Region

        #region Functions
        public async Task<Estandar<Departamento>> GetDepartamentos()
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Departamento>> response = new Response<Estandar<Departamento>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Departamento>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0005-geo-data/geo-region/list-by-country?idcountry=52");
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Departamento>)response.body;
        }
        public async Task<Estandar<Ciudad>> GetCiudades(int departamento)
        {
            Response<bool> connection = new Response<bool>();
            connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Ciudad>> response = new Response<Estandar<Ciudad>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Ciudad>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0005-geo-data/geo-city/list-by-region?idregion=" + departamento);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Ciudad>)response.body;
        }
        #endregion

        #endregion

        #region Module - Establishment

        #region Properties
        public EstablishmentsViewModel Establishments
        {
            get;
            set;
        }
        public EstablishmentsDetailViewModel EstablishmentsDetail
        {
            get;
            set;
        }
        #endregion

        #region Functions
        public async Task<Estandar<Establishment>> GetEstablishments(int idcity, int currentPag, int numRecordsByPag)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Establishment>> response = new Response<Estandar<Establishment>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Establishment>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-establishment/list-by-city?idcity=" + idcity + "&numRecordsByPag=" + numRecordsByPag + "&currentPag=" + currentPag);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Establishment>)response.body;
        }
        public async Task<Estandar<EstablishmentUrl>> GetParamUrlEstablishment()
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<EstablishmentUrl>> response = new Response<Estandar<EstablishmentUrl>>();
            if (connection.checkResponse)
            {
                EstablishmentUrl establishmentUrl = new EstablishmentUrl();
                response = await this.ApiService.Post<Estandar<EstablishmentUrl>, EstablishmentUrl>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0004-config-manager/config-param/get-param-url-establishment", establishmentUrl);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<EstablishmentUrl>)response.body;
        }
        #endregion

        #endregion

        #region Module - Requirement
        #region Atributes
        #endregion

        #region Properties
        public RequirementsViewModel Requirements
        {
            get;
            set;
        }
        public RequirementsDetailViewModel RequirementsDetail
        {
            get;
            set;
        }
        #endregion

        #region Functions
        public async Task<Estandar<Requirement>> GetRequirements(int currentPag, int numRecordsByPag)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Requirement>> response = new Response<Estandar<Requirement>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Requirement>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-requirement/list-all?numRecordsByPag=" + numRecordsByPag + "&currentPag=" + currentPag,
                    "Authorization", MainViewModel.GetInstance().Sesion.token);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Requirement>)response.body;
        }
        public async Task<Estandar<Requirement>> GetRequirementsSearch(string value, int currentPag, int numRecordsByPag)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Requirement>> response = new Response<Estandar<Requirement>>();
            EstandarPostSearch requirementPostSearch = new EstandarPostSearch()
            {
                query = value,
                currentPag = currentPag,
                numRecordsByPag = numRecordsByPag
            };
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<Requirement>, EstandarPostSearch>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-requirement/search-all",
                    "Authorization",MainViewModel.GetInstance().Sesion.token, requirementPostSearch);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Requirement>)response.body;
        }
        public async Task<Estandar<Requirement>> PostRequirementsById(RequirementsItemViewModel requirementsItemViewModel)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Requirement>> response = new Response<Estandar<Requirement>>();
            EstandarPost requirementPost = new EstandarPost()
            {
                id = requirementsItemViewModel.id,
                //sessionToken = Settings.Token
            };
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<Requirement>, EstandarPost>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0002-info-coljuegos/info-requirement/detail", "Authorization", MainViewModel.GetInstance().Sesion.token, requirementPost);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Requirement>)response.body;
        }
        #endregion

        #region Methods
        #endregion
        #endregion

        #region Module - Notification
        #region Atributes
        #endregion

        #region Properties
        public NotificationsViewModel Notifications
        {
            get;
            set;
        }

        public NotificationsDetailViewModel NotificationsDetail
        {
            get;
            set;
        }
        #endregion

        #region Functions
        public async Task<Estandar<Notification>> GetCheckNewNotifications(string username, int currentPag, int numRecordsByPag)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Notification>> response = new Response<Estandar<Notification>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Notification>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0006-notifications/notif-notification/check-new-notifications-by-user?username=" + username + "&numRecordsByPag=" + numRecordsByPag + "&currentPag=" + currentPag,
                    "Authorization", MainViewModel.GetInstance().Sesion.token);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Notification>)response.body;
        }
        public async Task<Estandar<Notification>> GetNotificationsAll(string username, int currentPag, int numRecordsByPag)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Notification>> response = new Response<Estandar<Notification>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Notification>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0006-notifications/notif-notification/list-all-by-user?username=" + username +"&numRecordsByPag=" + numRecordsByPag + "&currentPag=" + currentPag,
                    "Authorization", MainViewModel.GetInstance().Sesion.token);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Notification>)response.body;
        }
        public async Task<Estandar<Notification>> GetNotificationsUnRead(string username, int currentPag, int numRecordsByPag)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Notification>> response = new Response<Estandar<Notification>>();
            if (connection.checkResponse)
            {
                response = await this.ApiService.Get<Estandar<Notification>>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0006-notifications/notif-notification/list-all-by-user-unread?username=" + username + "&numRecordsByPag=" + numRecordsByPag + "&currentPag=" + currentPag,
                    "Authorization", MainViewModel.GetInstance().Sesion.token);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Notification>)response.body;
        }
        public async Task<Estandar<Notification>> PostNotificationsById(NotificationsItemViewModel notificationsItemViewModel)
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<Notification>> response = new Response<Estandar<Notification>>();
            EstandarPost notificationPost = new EstandarPost()
            {
                id = notificationsItemViewModel.id,
            };
            if (connection.checkResponse)
            {
                response = await this.ApiService.Post<Estandar<Notification>, EstandarPost>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0006-notifications/notif-notification/detail", "Authorization", MainViewModel.GetInstance().Sesion.token, notificationPost);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<Notification>)response.body;
        }
        #endregion

        #region Methods
        public async void SyncNotifications()
        {
            Estandar<Notification> NotificationUnread = await GetCheckNewNotifications(Sesion.username, 0, 10);
            if (NotificationUnread != null)
            {
                foreach (var item in NotificationUnread.lst)
                {
                    SendNotification(item.subject, item.message);
                }
            }
        }
        #endregion
        #endregion

        #region Module - PQRS
        #region Functions
        public async Task<Estandar<PQRSUrl>> GetParamUrlPQRS()
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<PQRSUrl>> response = new Response<Estandar<PQRSUrl>>();
            if (connection.checkResponse)
            {
                PQRSUrl pQRSUrl = new PQRSUrl();
                response = await this.ApiService.Post<Estandar<PQRSUrl>, PQRSUrl>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0004-config-manager/config-param/get-param-url-pqr", pQRSUrl);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<PQRSUrl>)response.body;
        }
        #endregion
        #endregion

        #region Module - Portal
        #region Functions
        public async Task<Estandar<PortalUrl>> GetParamUrlPortal()
        {
            Response<bool> connection = await this.ApiService.CheckConnection<bool>();
            Response<Estandar<PortalUrl>> response = new Response<Estandar<PortalUrl>>();
            if (connection.checkResponse)
            {
                PortalUrl portalUrl = new PortalUrl();
                response = await this.ApiService.Post<Estandar<PortalUrl>, PortalUrl>(
                    MainViewModel.GetInstance().URLAPI +
                    "/service/s0004-config-manager/config-param/get-param-url-portaloper", portalUrl);
            }
            if (!response.checkResponse)
            {
                return null;
            }
            if (response.body == null)
            {
                return null;
            }
            return (Estandar<PortalUrl>)response.body;
        }
        #endregion
        #endregion

        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion


        //BASE

        #region Atributes
        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        #endregion

    }
}
