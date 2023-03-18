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

    public class ChangePasswordViewModel : BaseViewModel
    {
        #region Attributes
        private string email;
        private string oldpassword;
        private string password;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Oldpassword
        {
            get { return this.oldpassword; }
            set { SetValue(ref this.oldpassword, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }
        #endregion

        #region Constructors
        public ChangePasswordViewModel(string email, string oldpassword)
        {
            this.Email = email;
            this.Oldpassword = oldpassword;
        }
        #endregion

        #region Commands
        public ICommand NewChangePasswordCommand
        {
            get
            {
                return new Command(() => NewChangePassword());
            }
        }

        #endregion

        #region Methods
        private async Task<bool> Validate()
        {
            try
            {
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
                Match matchEspeciales = Regex.Match(Password, @"[ñÑ\-_¿.#¡@$%!&*]");
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
        private async void NewChangePassword()
        {
            this.IsRunning = true;
            if (await Validate())
            {
                UserChange userChange = new UserChange();
                userChange.username = Email;
                userChange.tmpPassword = Oldpassword;
                userChange.newPassword= Password;

                Estandar<UserChange> estandar = await MainViewModel.GetInstance().PostChangePassword(userChange);
                if (estandar != null)
                {
                    if (!estandar.processIsSuccessful)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                                "Error",
                                "No se ha podido cambiar la contraseña: " + estandar.msg,
                                "Aceptar");
                        this.IsRunning = false;
                        return;
                    }
                    await Application.Current.MainPage.DisplayAlert(
                                "Información",
                                "Se ha cambiado la contraseña satisfactoriamente!",
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
