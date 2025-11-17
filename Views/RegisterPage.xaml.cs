using PlayMatch.Models;

namespace PlayMatch.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void BtnRegister_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim() ?? "";
        string pass = PasswordEntry.Text ?? "";
        string confirm = ConfirmEntry.Text ?? "";

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(pass) ||
            string.IsNullOrWhiteSpace(confirm))
        {
            await DisplayAlert("Error", "Completa todos los campos.", "OK");
            return;
        }

        if (!email.Contains("@") || !email.Contains("."))
        {
            await DisplayAlert("Error", "Correo no válido.", "OK");
            return;
        }

        if (pass.Length < 6)
        {
            await DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres.", "OK");
            return;
        }

        if (pass != confirm)
        {
            await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
            return;
        }

        var newUser = new User
        {
            Email = email,
            Password = pass,
            FullName = "Nuevo usuario",
            CreatedAt = DateTime.UtcNow
        };

        bool ok = await App.Database.AddUserAsync(newUser);

        if (!ok)
        {
            await DisplayAlert("Error", "Este correo ya está registrado.", "OK");
            return;
        }

        await DisplayAlert("Éxito", "Cuenta creada correctamente.", "OK");
        await Shell.Current.GoToAsync("//LoginPage");
    }

    private async void OnLoginTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
