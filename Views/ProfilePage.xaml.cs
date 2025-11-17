using PlayMatch.Models;

namespace PlayMatch.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        LoadUser();
    }

    private void LoadUser()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                var u = await App.Database.GetUserAsync("cuentaplaymatch@gmail.com", "usuariomatch");
                if (u != null)
                {
                    LblName.Text = u.FullName;
                    LblBio.Text = $"Registrado: {u.CreatedAt:yyyy-MM-dd}";
                }
            }
            catch
            {
                // Ignorar errores
            }
        });
    }

    private async void BtnEdit_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Editar", "Funcionalidad de editar perfil (pendiente).", "OK");
    }

    private async void BtnLogout_Clicked(object sender, EventArgs e)
    {
        await AppShell.LogoutAsync();
    }
}
