using APP.Models;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class RequirementsItemViewModel : Requirement
    {
        public string CONTENIDO_INTRO { get; set; }

        #region Commands
        public ICommand SelectRequirementsItemCommand
        {
            get
            {
                return new Command(() => LoadRequirementsItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadRequirementsItem()
        {
            MainViewModel.GetInstance().RequirementsDetail = new RequirementsDetailViewModel(this);
            await App.Navigator.PushAsync(new RequirementsDetailPage());
        }
        #endregion
    }
}
