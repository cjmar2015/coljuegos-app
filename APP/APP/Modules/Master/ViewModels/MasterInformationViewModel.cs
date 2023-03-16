namespace APP.ViewModels
{
    using System.Windows.Input;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class MasterInformationViewModel : BaseViewModel
    {
        #region Attributes
        private string currentVersion;
        #endregion

        #region Properties
        public string CurrentVersion
        {
            get { return this.currentVersion; }
            set { SetValue(ref this.currentVersion, value); }
        }
        #endregion

        #region Constructors
        public MasterInformationViewModel()
        {
            CurrentVersion = MainViewModel.GetInstance().CurrentVersion;
        }
        #endregion

        #region Commands
        
        public ICommand OpenWebCommand
        {
            get
            {
                return new Command(() => OpenWeb());
            }
        }
        #endregion

        #region Methods
        private async void OpenWeb()
        {
            await Launcher.OpenAsync("https://www.coljuegos.gov.co/");
        }
        #endregion
    }
}
