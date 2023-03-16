namespace APP.ViewModels
{
    using APP.Helpers;
    using APP.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MasterMenuItemViewModel
    {
        #region Propperties
        public string Icon
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string PägeName
        {
            get; set;
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new Command(() => Navigate());
            }
        }
        #endregion

        #region Methods
        private async void Navigate()
        {
            App.Master.IsPresented = false;
            if (this.PägeName == "Información")
            {
                MainViewModel.GetInstance().MasterInformation = new MasterInformationViewModel();
                await App.Navigator.PushAsync(new MasterInformationPage());
            }
            if (this.PägeName == "Requirement")
            {
                MainViewModel.GetInstance().Requirements = new RequirementsViewModel();
                await App.Navigator.PushAsync(new RequirementsPage());
            }
            if (this.PägeName == "LoginPage")
            {
                //Settings.Email = string.Empty;
                //Settings.Password = string.Empty;
                //Settings.UserID = string.Empty;
                Settings.IsLogin = false;
                //MainViewModel.GetInstance().Email = string.Empty;
                //MainViewModel.GetInstance().Password = string.Empty;
                MainViewModel.GetInstance().MasterLogin = new MasterLoginViewModel();
                await App.Navigator.PushAsync(new MasterLoginPage());
            }
            if (this.PägeName == "ClosePage")
            {
                //Settings.Email = string.Empty;
                //Settings.Password = string.Empty;
                //Settings.UserID = string.Empty;
                Settings.IsLogin = false;
                MainViewModel.GetInstance().Sesion = null;
                //MainViewModel.GetInstance().Email = string.Empty;
                //MainViewModel.GetInstance().Password = string.Empty;
                MainViewModel.GetInstance().LoadMasterMenu();
            }
        }
        #endregion
    }
}