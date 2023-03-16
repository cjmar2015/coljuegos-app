using APP.Models;
using APP.Modules.Establecimientos.Models;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class EstablishmentsItemViewModel : Establishment
    {

        #region Commands
        public ICommand SelectEstablishmentsItemCommand
        {
            get
            {
                return new Command(() => LoadEstablishmentsItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadEstablishmentsItem()
        {
            MainViewModel.GetInstance().EstablishmentsDetail = new EstablishmentsDetailViewModel(this);
            await App.Navigator.PushAsync(new EstablishmentsDetailPage());
        }
        #endregion
    }
}
