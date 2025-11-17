using PlayMatch.Models;

namespace PlayMatch.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        string email = LoginEmail.Text?.Trim() ?? "";
        string pass = LoginPassword.Text ?? "";

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
        {
            await DisplayAlert("Error", "Por favor ingresa tu correo y contraseña.", "OK");
            return;
        }

        var user = await App.Database.GetUserAsync(email, pass);

        if (user == null)
        {
            await DisplayAlert("Error", "Correo o contraseña incorrectos.", "OK");
            return;
        }

        // Ir al Home
        await Shell.Current.GoToAsync("//HomePage");
    }

    private void BtnGoogle_Clicked(object sender, EventArgs e)
    {
        // Se implementará más adelante
    }

    private async void OnRegisterTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    }

    private async void OnForgotTapped(object sender, EventArgs e)
    {
        await DisplayAlert("Recuperar contraseña",
            "Esta función estará disponible en una próxima versión.",
            "OK");
    }
}
