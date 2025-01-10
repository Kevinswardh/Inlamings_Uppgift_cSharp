namespace MauiXamlApp1.Views.AuthenticationPages
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Navigera tillbaka till MainPage
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
