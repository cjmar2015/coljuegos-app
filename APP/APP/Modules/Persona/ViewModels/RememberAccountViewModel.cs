namespace APP.ViewModels
{
    using Models;
    using APP.Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    using static Xamarin.Essentials.Permissions;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System;
    using Xamarin.Essentials;

    public class RememberAccountViewModel : BaseViewModel
    {
        #region Attributes
        private string email;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        #endregion

        #region Constructors
        public RememberAccountViewModel()
        {

        }
        #endregion

        #region Commands
        public ICommand RememberAccountCommand
        {
            get
            {
                return new Command(() => RememberAccount());
            }
        }
        #endregion

        #region Methods
        private async Task<bool> Validate()
        {
            try
            {
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
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private async void RememberAccount()
        {
            this.IsRunning = true;
            if (await Validate())
            {
                User user = new User();
                user.email = Email;
                Estandar<User> estandar = await MainViewModel.GetInstance().PostRememberPassword(user);
                if (estandar != null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                                "Información",
                                estandar.msg.ToString(),
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
