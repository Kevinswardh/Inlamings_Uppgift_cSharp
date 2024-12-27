using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WPF_Mvvm_Version.Views.HomePage;

namespace WPF_Mvvm_Version.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;

        public ICommand NavigateHomeCommand { get; }

        public MainWindowViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            // Initiera kommandon
            NavigateHomeCommand = new RelayCommand(NavigateHome);

            // Navigera till standard sidan
            NavigateHome();
        }

        private void NavigateHome()
        {
            if (_navigationService != null)
            {
                _navigationService.Navigate(new HomePage());
            }
        }
    }
}
