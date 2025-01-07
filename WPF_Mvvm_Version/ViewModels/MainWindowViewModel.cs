using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WPF_Mvvm_Version.Views.HomePage;

namespace WPF_Mvvm_Version.ViewModels
{
    /// <summary>
    /// ViewModel för MainWindow som hanterar navigering och kommandon.
    /// </summary>
    public class MainWindowViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;

        /// <summary>
        /// Kommando för att navigera till startsidan.
        /// </summary>
        public ICommand NavigateHomeCommand { get; }

        /// <summary>
        /// Konstruktor för MainWindowViewModel.
        /// </summary>
        /// <param name="navigationService">NavigationService används för att hantera sidnavigering.</param>
        public MainWindowViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            // Initiera kommando för att navigera till startsidan.
            NavigateHomeCommand = new RelayCommand(NavigateHome);

            // Navigera till standard startsidan vid initialization.
            NavigateHome();
        }

        /// <summary>
        /// Navigerar till startsidan.
        /// </summary>
        private void NavigateHome()
        {
            if (_navigationService != null)
            {
                // Navigerar till HomePage.
                _navigationService.Navigate(new HomePage());
            }
        }
    }
}
