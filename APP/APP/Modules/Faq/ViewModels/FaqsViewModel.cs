namespace APP.ViewModels
{
    using APP.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class FaqsViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<FaqsItemViewModel> objFaqs;
        private String filter;
        #endregion

        #region Properties
        public ObservableCollection<FaqsItemViewModel> ObjFaqs
        {
            get { return this.objFaqs; }
            set { SetValue(ref this.objFaqs, value); }
        }
        public Estandar<Faq> Faq
        {
            get;
            set;
        }
        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
            }
        }
        #endregion

        #region Constructors
        public FaqsViewModel()
        {
            LoadFaqs();
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new Command(() => Search());
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() => Search());
            }
        }
        #endregion

        #region Methods
        public async void LoadFaqs()
        {
            this.IsRunning = true;
            Faq = await MainViewModel.GetInstance().GetFaqs();
            this.IsRunning = false;
            if (Faq != null)
            {
                this.ObjFaqs = new ObservableCollection<FaqsItemViewModel>(
                 this.ToFaqsItemViewModel());
            }
        }
        public async void LoadFaqsSearch()
        {
            this.IsRunning = true;
            Faq = await MainViewModel.GetInstance().PostFaqsSearch(this.Filter, 0, 10);
            this.IsRunning = false;
            if (Faq != null)
            {
                this.ObjFaqs = new ObservableCollection<FaqsItemViewModel>(
            this.ToFaqsItemViewModel());
            }
        }
        public void Search()
        {
            if (!string.IsNullOrEmpty(this.Filter))
            {
                this.LoadFaqsSearch();
            }
            else
            {
                this.LoadFaqs();
            }
        }
        private IEnumerable<FaqsItemViewModel> ToFaqsItemViewModel()
        {
            return Faq.lst.Select(l => new FaqsItemViewModel
            {
                id = l.id,
                question = l.question,
                response = l.response,
                published = l.published,
                updatedAt = l.updatedAt,
                userUpdated = l.userUpdated,
                sessionToken = l.sessionToken,
                CONTENIDO_INTRO = MainViewModel.GetInstance().ValueContenidoIntro(l.response),
            });
        }
        #endregion
    }
}
