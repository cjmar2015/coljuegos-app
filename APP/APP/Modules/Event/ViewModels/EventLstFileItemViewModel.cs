using APP.Models;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class EventLstFileItemViewModel : EstandarLstFile
    {
        #region Properties
        public bool IMAGEN_VIEW
        {
            get;
            set;
        }
        public bool VIDEO_VIEW
        {
            get;
            set;
        }
        #endregion

        #region Commands
        public ICommand SelectEventLstFileItemCommand
        {
            get
            {
                return new Command(() => LoadEventLstFileItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadEventLstFileItem()
        {

        }
        #endregion
    }
}
