namespace APP.ViewModels
{
    using APP.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class RequirementsDetailViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<RequirementsDetailItemViewModel> objRequirementItems;
        private Estandar<Requirement> requirement;
        #endregion

        #region Properties
        public ObservableCollection<RequirementsDetailItemViewModel> ObjRequirementItems
        {
            get { return this.objRequirementItems; }
            set { SetValue(ref this.objRequirementItems, value); }
        }
        public Estandar<Requirement> Requirement
        {
            get { return this.requirement; }
            set { SetValue(ref this.requirement, value); }
        }
        public RequirementsItemViewModel RequirementsItem
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public RequirementsDetailViewModel(RequirementsItemViewModel requirementsItemViewModel)
        {
            RequirementsItem = requirementsItemViewModel;
            LoadRequirements();
        }
        #endregion

        #region Methods
        public async void LoadRequirements()
        {
            this.IsRunning = true;
            this.Requirement = await MainViewModel.GetInstance().PostRequirementsById(RequirementsItem);
            this.IsRunning = false;
            if (Requirement != null)
            {
                this.ObjRequirementItems = new ObservableCollection<RequirementsDetailItemViewModel>(
                 this.ToRequirementsDetailItemViewModel());
            }
        }
        private IEnumerable<RequirementsDetailItemViewModel> ToRequirementsDetailItemViewModel()
        {
            return Requirement.obj.lstItem.Select(l => new RequirementsDetailItemViewModel
            {
                id = l.id,
                itemTitle = l.itemTitle,
                description = l.description,
                updatedAt = l.updatedAt,
                userUpdated = l.userUpdated,
                requerimentId = l.requerimentId,
                sessionToken = l.sessionToken,
            });
        }
        #endregion
    }
}
