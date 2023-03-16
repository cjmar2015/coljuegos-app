namespace APP.ViewModels
{
    using APP.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class FaqsDetailViewModel : BaseViewModel
    {
        #region Attributes
        
        private Estandar<Faq> faq;
        #endregion

        #region Properties
        public Estandar<Faq> Faq
        {
            get { return this.faq; }
            set { SetValue(ref this.faq, value); }
        }

        public FaqsItemViewModel FaqsItem
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public FaqsDetailViewModel(FaqsItemViewModel faqsItemViewModel)
        {
            FaqsItem = faqsItemViewModel;
            LoadFaqs();
        }
        #endregion

        #region Methods
        public async void LoadFaqs()
        {
            this.IsRunning = true;
            this.Faq = await MainViewModel.GetInstance().PostFaqsById(FaqsItem);
            this.IsRunning = false;
        }
        #endregion
    }
}
