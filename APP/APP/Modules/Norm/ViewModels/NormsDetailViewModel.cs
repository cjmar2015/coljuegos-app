namespace APP.ViewModels
{
    using APP.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class NormsDetailViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<NormLstFileItemViewModel> normLstFiles;
        private Estandar<Norm> norm;
        #endregion

        #region Properties
        public ObservableCollection<NormLstFileItemViewModel> NormLstFiles
        {
            get { return this.normLstFiles; }
            set { SetValue(ref this.normLstFiles, value); }
        }
        public Estandar<Norm> Norm
        {
            get { return this.norm; }
            set { SetValue(ref this.norm, value); }
        }

        public NormsItemViewModel NormsItem
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public NormsDetailViewModel(NormsItemViewModel normsItemViewModel)
        {
            NormsItem = normsItemViewModel;
            LoadNorms();
        }
        #endregion

        #region Methods
        public async void LoadNorms()
        {
            this.IsRunning = true;
            this.Norm = await MainViewModel.GetInstance().PostNormsById(NormsItem);
            this.IsRunning = false;
            this.NormLstFiles = new ObservableCollection<NormLstFileItemViewModel>(
             this.ToNormLstFileItemViewModel());
        }
        private IEnumerable<NormLstFileItemViewModel> ToNormLstFileItemViewModel()
        {
            return Norm.obj.lstFiles.Select(l => new NormLstFileItemViewModel
            {
                id = l.id,
                url = l.url,
                nameFile = l.nameFile,
                contentB64 = l.contentB64,
                type = l.type,
                description = l.description,
                updatedAt = l.updatedAt,
                userUpdated = l.userUpdated,
                sessionToken = l.sessionToken
            });
        }
        #endregion
    }
}
