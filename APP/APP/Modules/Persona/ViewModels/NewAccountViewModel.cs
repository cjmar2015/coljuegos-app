namespace APP.ViewModels
{
    using Models;
    using APP.Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using System.Threading.Tasks;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class NewAccountViewModel : BaseViewModel
    {
        #region Attributes
        private string name;
        private string lastName;
        private string phone;
        private string email;
        private string password;
        #endregion

        #region Properties
        public string Name
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }
        public string LastName
        {
            get { return this.lastName; }
            set { SetValue(ref this.lastName, value); }
        }
        public string Phone
        {
            get { return this.phone; }
            set { SetValue(ref this.phone, value); }
        }
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
        #endregion

        #region Constructors
        public NewAccountViewModel()
        {

        }
        #endregion

        #region Commands
        public ICommand NewAccountCommand
        {
            get
            {
                return new Command(() => NewAccount());
            }
        }

        #endregion

        #region Methods
        private async Task<bool> Validate()
        {
            try
            {
                if (string.IsNullOrEmpty(Name))
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Ingrese su nombre",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }
                if (string.IsNullOrEmpty(LastName))
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Ingrese sus apellidos",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }
                if (string.IsNullOrEmpty(Phone) || Phone.Count() < 10)
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Ingrese un número de teléfono válido",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }

                if (string.IsNullOrEmpty(Email))
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Ingrese un email válido",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }
                else
                {
                    String sFormato;
                    sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                    if (!Regex.IsMatch(Email, sFormato))
                    {
                        await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Ingrese un email válido",
                            "Aceptar");
                        this.IsRunning = false;
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(Password))
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Ingrese un password válido",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }
                Match matchLongitud = Regex.Match(Password, @"^\w{7,15}\b");
                Match matchNumeros = Regex.Match(Password, @"\d");
                Match matchEspeciales = Regex.Match(Password, @"[ñÑ\-_¿.#¡@$%!&]");
                Match matchMayusculas = Regex.Match(Password, @"[A-Z]");
                Match matchMinusculas = Regex.Match(Password, @"[a-z]");
                if (!matchLongitud.Success)
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "Ingrese un password de 7 a 15 carácteres",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }
                if (!matchNumeros.Success)
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "El password debe tener almenos 1 número",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }
                if (!matchEspeciales.Success)
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "El password debe tener almenos 1 carácter especial",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }
                if (!matchMayusculas.Success)
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "El password debe tener almenos 1 mayúscula",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }
                if (!matchMinusculas.Success)
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Error",
                            "El password debe tener almenos 1 minúscula",
                            "Aceptar");
                    this.IsRunning = false;
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private async void NewAccount()
        {
            this.IsRunning = true;
            if (await Validate())
            {
                User user = new User();
                user.email = Email;
                user.newPassword= Password;
                user.person = new Person()
                {
                    name = Name,
                    lastName = Name,
                    phoneNumber = Phone
                };
                Estandar<User> estandar = await MainViewModel.GetInstance().PostNewAccount(user);
                if (estandar != null)
                {
                    if (!estandar.processIsSuccessful)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                                "Error",
                                "No se ha podido crear la cuenta: " + estandar.msg,
                                "Aceptar");
                        this.IsRunning = false;
                        return;
                    }
                    await Application.Current.MainPage.DisplayAlert(
                                "Información",
                                "Creado con éxito, hemos enviado un correo electrónico a la dirección " + Email + " para verificarlo. Debe verificar su correo electrónico para ingresar al sistema.",
                                "Aceptar");
                        this.IsRunning = false;
                    
                    await App.Navigator.PopAsync();
                }
            }
            this.IsRunning = false;
        }
        #endregion
    }
}
