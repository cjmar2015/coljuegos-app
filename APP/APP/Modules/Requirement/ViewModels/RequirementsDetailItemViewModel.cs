using APP.Models;
using APP.Modules.Requirement.Models;
using APP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace APP.ViewModels
{
    public class RequirementsDetailItemViewModel : RequirementItem
    {
        public string CONTENIDO_INTRO { get; set; }

        #region Commands
        public ICommand SelectRequirementsDetailItemCommand
        {
            get
            {
                return new Command(() => LoadRequirementsDetailItem());
            }
        }
        #endregion

        #region Methods
        private async void LoadRequirementsDetailItem()
        {

        }
        #endregion
    }
}
