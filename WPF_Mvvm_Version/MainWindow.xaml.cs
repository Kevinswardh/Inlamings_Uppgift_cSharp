using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Mvvm_Version.ViewModels;
using WPF_Mvvm_Version.Views;
using WPF_Mvvm_Version.Views.HomePage;

namespace WPF_Mvvm_Version
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Kontrollera att ContentFrame inte är null
            if (ContentFrame != null)
            {
                // Tilldela NavigationService till App-klassen
                App.SetNavigationService(ContentFrame.NavigationService);

                // Ställ in ViewModel för MainWindow
                DataContext = new MainWindowViewModel(ContentFrame.NavigationService);
            }
            else
            {
                MessageBox.Show("ContentFrame är null! Kontrollera MainWindow.xaml.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Avsluta hela applikationen
            Environment.Exit(0);
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }


}