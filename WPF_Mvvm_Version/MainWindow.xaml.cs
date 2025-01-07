using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_Mvvm_Version.ViewModels;

namespace WPF_Mvvm_Version
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// Huvudfönstret i applikationen som hanterar UI och navigering.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialiserar en ny instans av MainWindow-klassen.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Kontrollera att ContentFrame inte är null innan navigering hanteras.
            if (ContentFrame != null)
            {
                // Tilldela NavigationService till App-klassen för global åtkomst.
                App.SetNavigationService(ContentFrame.NavigationService);

                // Ställ in ViewModel för MainWindow.
                DataContext = new MainWindowViewModel(ContentFrame.NavigationService);
            }
            else
            {
                // Visa ett felmeddelande om ContentFrame inte hittas.
                MessageBox.Show("ContentFrame är null! Kontrollera MainWindow.xaml.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Hanterar klick på stäng-knappen i applikationen.
        /// Avslutar applikationen när knappen klickas.
        /// </summary>
        /// <param name="sender">Objektet som utlöste händelsen.</param>
        /// <param name="e">Händelsedata för klickhändelsen.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Avsluta hela applikationen.
            Environment.Exit(0);
        }

        /// <summary>
        /// Tillåter användaren att dra fönstret genom att klicka och hålla ned musknappen på headern.
        /// </summary>
        /// <param name="sender">Objektet som utlöste händelsen.</param>
        /// <param name="e">Händelsedata för musknappens tillstånd.</param>
        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Kontrollera om vänster musknapp är nedtryckt.
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Tillåt att fönstret dras.
                this.DragMove();
            }
        }
    }
}
