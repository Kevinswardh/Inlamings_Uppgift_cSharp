using System.Windows.Controls;
using System.Windows;
using WPF_Mvvm_Version.Views.MainWindows.AuthenticationPages;
using WPF_Mvvm_Version.ViewModels.MainWindows.HomePage;

namespace WPF_Mvvm_Version.Views.HomePage
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel();
          
        }

    }
}
