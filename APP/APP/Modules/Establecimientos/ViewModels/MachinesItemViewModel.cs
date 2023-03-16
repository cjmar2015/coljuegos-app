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
    public class MachinesItemViewModel : Machine
    {

        #region Commands
        public ICommand SelectMachinesItemCommand
        {
            get
            {
                return new Command(() => LoadMachinesItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadMachinesItem()
        {

        }
        #endregion
    }
}
