namespace MauiXamlApp1.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void NavigateToRegister(object sender, EventArgs e)
        {
            // Navigera till RegisterPage
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private void CloseApp(object sender, EventArgs e)
        {
#if WINDOWS
            Application.Current.Quit();
#endif
        }
    }
}
