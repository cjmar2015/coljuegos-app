namespace APP.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private bool isVisible;
        private bool isRunning;
        private bool isEnabled;
        private string mensaje;
        private bool viewMensaje;
        private bool viewBuscar;
        private string title;
        private DateTime minDateIni;
        private DateTime maxDateIni;
        private DateTime dateIni;
        #endregion

        #region Properties
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public string Mensaje
        {
            get { return this.mensaje; }
            set { SetValue(ref this.mensaje, value); }
        }
        public bool ViewMensaje
        {
            get { return this.viewMensaje; }
            set { SetValue(ref this.viewMensaje, value); }
        }
        public bool ViewBuscar
        {
            get { return this.viewBuscar; }
            set { SetValue(ref this.viewBuscar, value); }
        }
        public string Title
        {
            get { return this.title; }
            set
            {
                SetValue(ref this.title, value);
            }
        }
        public DateTime MinDateIni
        {
            get { return this.minDateIni; }
            set { SetValue(ref this.minDateIni, value); }
        }
        public DateTime MaxDateIni
        {
            get { return this.maxDateIni; }
            set { SetValue(ref this.maxDateIni, value); }
        }
        public DateTime DateIni
        {
            get { return this.dateIni; }
            set
            {
                SetValue(ref this.dateIni, value);
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }
            backingField = value;
            OnPropertyChanged(propertyName);
        }
    }
}