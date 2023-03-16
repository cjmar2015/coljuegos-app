using APP.Models;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class NormLstFileItemViewModel : EstandarLstFile
    {
        #region Properties

        #endregion

        #region Commands
        public ICommand SelectNormLstFileItemCommand
        {
            get
            {
                return new Command(() => LoadNormLstFileItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadNormLstFileItem()
        {

        }
        #endregion
    }
}
