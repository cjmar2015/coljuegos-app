namespace APP.ViewModels
{
    using APP.Views;
    using Helpers;
    using Models;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MasterLoginViewModel : BaseViewModel
    {
        #region Events

        #endregion
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string email;
        private string password;
        private string currentVersion;
        private bool isPassword;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        public string CurrentVersion
        {
            get { return this.currentVersion; }
            set { SetValue(ref this.currentVersion, value); }
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsPassword
        {
            get { return this.isPassword; }
            set { SetValue(ref this.isPassword, value); }
        }
        #endregion

        #region Constructors
        public MasterLoginViewModel()
        {
            this.IsPassword = true;
            this.IsRemembered = true;
            this.IsEnabled = false;
            this.apiService = new ApiService();
            this.Mensaje = "Iniciar sesión";
            CurrentVersion = MainViewModel.GetInstance().CurrentVersion;
            if (!string.IsNullOrEmpty(Settings.Email))
            {
                this.Email = Settings.Email;
                this.Password = Settings.Password;
                //LoginCommand.Execute(this);
            }
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new Command(() => Login());
            }
        }
        public ICommand RememberCommand
        {
            get
            {
                return new Command(() => Remember());
            }
        }
        public ICommand NewAccountCommand
        {
            get
            {
                return new Command(() => NewAccount());
            }
        }
        public ICommand LockCommand
        {
            get
            {
                return new Command(() => LoadLockCommand());
            }
        }
        public ICommand CerrarCommand
        {
            get
            {
                return new Command(() => Close());
            }
        }
        #endregion

        #region Methods
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar el usuario",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar el Password",
                    "Aceptar");
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;

            ////Verificar conexion
            Response<bool> connection = await this.apiService.CheckConnection<bool>();
            if (!connection.checkResponse)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.message,
                    "Aceptar");
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            this.Email = this.Email.Replace(" ", "");
            Login login = new Login()
            {
                username = Email,
                password = Password
            };
            Sesion _sesion = await MainViewModel.GetInstance().PostLogin(login);

            if (_sesion == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                "Error de acceso",
                "Los datos de acceso son incorrectos.",
                "Aceptar");
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            //pesistencia
            MainViewModel.GetInstance().Sesion = _sesion;

            if (IsRemembered)
            {
                Settings.UserID = _sesion.username;
                Settings.Token = _sesion.token;
                Settings.Email = this.Email;
                Settings.Password = this.Password;
                Settings.IsLogin = true;
            }
            else
            {
                Settings.UserID = string.Empty;
                Settings.Token = string.Empty;
                Settings.Email = string.Empty;
                Settings.Password = string.Empty;
                Settings.IsLogin = false;
            }

            this.Email = string.Empty;
            this.Password = string.Empty;

            _ = await MainViewModel.GetInstance().LoadPersonaReload();

            this.IsRunning = false;
            this.IsEnabled = true;
            await App.Navigator.PopAsync();
        }
        private async void Remember()
        {
            MainViewModel.GetInstance().RememberAccount = new RememberAccountViewModel();
            await App.Navigator.PushAsync(new RememberAccountPage());
        }
        private async void NewAccount()
        {
            MainViewModel.GetInstance().NewAccount = new NewAccountViewModel();
            await App.Navigator.PushAsync(new NewAccountPage());
        }
        private void LoadLockCommand()
        {
            if (IsPassword == true)
            {
                IsPassword = false;
            }
            else
            {
                IsPassword = true;
            }
        }
        private async void Close()
        {
            Settings.Email = string.Empty;
            Settings.Password = string.Empty;
            Settings.UserID = string.Empty;
            Settings.IsLogin = false;
            MainViewModel.GetInstance().Email = string.Empty;
            MainViewModel.GetInstance().Password = string.Empty;
            MainViewModel.GetInstance().LoadMasterMenu();
        }
        #endregion
    }
}
