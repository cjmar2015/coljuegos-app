using APP.Models;
using APP.Services;
using System;

namespace APP.ViewModels
{
    public class MasterWebViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string url;
        #endregion

        #region Properties
        public string Url
        {
            get { return this.url; }
            set
            {
                SetValue(ref this.url, value);
            }
        }
        #endregion
        #region Constructors
        public MasterWebViewModel(String _Url)
        {
            this.apiService = new ApiService();
            this.IsVisible = true;
            this.ViewBuscar = false;
            this.Url = _Url;
            CheckPage();
        }
        #endregion
        #region Methods
        private async void CheckPage()
        {
            Response<bool> connection = await this.apiService.CheckConnection<bool>();
            if (!connection.checkResponse)
            {
                this.Mensaje = "No hay conexión a internet.";
                this.IsVisible = false;
                this.ViewBuscar = true;
            }
        }
        #endregion
    }
}
