namespace APP.ViewModels
{
    using APP.Helpers;
    using APP.Views;
    using Models;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class MasterModuleItemViewModel
    {
        #region Propperties
        public string PägeName
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string Mensaje
        {
            get; set;
        }
        public string Valor
        {
            get; set;
        }
        public string BackgroundColor
        {
            get;
            set;
        }
        public string TextColor
        {
            get;
            set;
        }
        public string BorderColor
        {
            get;
            set;
        }
        public string Imagen
        {
            get;
            set;
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new Command(() => NavigateAsync());
            }
        }
        #endregion

        #region Methods
        private async Task NavigateAsync()
        {
            if (this.PägeName == "Events")
            {
                MainViewModel.GetInstance().Dashboards.ShowEventsCommand.Execute(this);
            }
            if (this.PägeName == "Faq")
            {
                MainViewModel.GetInstance().Dashboards.ShowFaqCommand.Execute(this);
            }
            if (this.PägeName == "PQRS")
            {
                MainViewModel.GetInstance().Dashboards.ShowPqrsCommand.Execute(this);
            }
            if (this.PägeName == "Portal")
            {
                MainViewModel.GetInstance().Dashboards.ShowPortalCommand.Execute(this);
            }
            if (this.PägeName == "Channel")
            {
                MainViewModel.GetInstance().Dashboards.ShowChannelCommand.Execute(this);
            }
            if (this.PägeName == "Norm")
            {
                MainViewModel.GetInstance().Dashboards.ShowNormsCommand.Execute(this);
            }
            if (this.PägeName == "Requirement")
            {
                if (MainViewModel.GetInstance().Sesion != null)
                {
                    MainViewModel.GetInstance().Dashboards.ShowRequirementsCommand.Execute(this);
                }
                else
                {
                    MainViewModel.GetInstance().MasterLogin = new MasterLoginViewModel();
                    await App.Navigator.PushAsync(new MasterLoginPage());
                }
            }
            if (this.PägeName == "Establishment")
            {
                MainViewModel.GetInstance().Dashboards.ShowEstablishmentsCommand.Execute(this);
            }
        }
        #endregion
    }
}
