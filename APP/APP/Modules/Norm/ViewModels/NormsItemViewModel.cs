using APP.Models;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class NormsItemViewModel : Norm
    {
        public string CONTENIDO_INTRO { get; set; }

        #region Commands
        public ICommand SelectNormsItemCommand
        {
            get
            {
                return new Command(() => LoadNormsItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadNormsItem()
        {
            MainViewModel.GetInstance().NormsDetail = new NormsDetailViewModel(this);
            await App.Navigator.PushAsync(new NormsDetailPage());
        }
        #endregion
    }
}
