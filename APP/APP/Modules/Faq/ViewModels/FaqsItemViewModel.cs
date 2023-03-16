using APP.Models;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class FaqsItemViewModel : Faq
    {
        public string CONTENIDO_INTRO { get; set; }

        #region Commands
        public ICommand SelectFaqsItemCommand
        {
            get
            {
                return new Command(() => LoadFaqsItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadFaqsItem()
        {
            MainViewModel.GetInstance().FaqsDetail = new FaqsDetailViewModel(this);
            await App.Navigator.PushAsync(new FaqsDetailPage());
        }
        #endregion
    }
}
