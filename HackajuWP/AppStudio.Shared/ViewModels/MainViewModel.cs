using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private FacebookViewModel _facebookModel;
       private TwitterViewModel _twitterModel;
       private InstagramViewModel _instagramModel;
       private SobreViewModel _sobreModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = FacebookModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public FacebookViewModel FacebookModel
        {
            get { return _facebookModel ?? (_facebookModel = new FacebookViewModel()); }
        }
 
        public TwitterViewModel TwitterModel
        {
            get { return _twitterModel ?? (_twitterModel = new TwitterViewModel()); }
        }
 
        public InstagramViewModel InstagramModel
        {
            get { return _instagramModel ?? (_instagramModel = new InstagramViewModel()); }
        }
 
        public SobreViewModel SobreModel
        {
            get { return _sobreModel ?? (_sobreModel = new SobreViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            FacebookModel.ViewType = viewType;
            TwitterModel.ViewType = viewType;
            InstagramModel.ViewType = viewType;
            SobreModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                FacebookModel.LoadItemsAsync(forceRefresh),
                TwitterModel.LoadItemsAsync(forceRefresh),
                InstagramModel.LoadItemsAsync(forceRefresh),
                SobreModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
