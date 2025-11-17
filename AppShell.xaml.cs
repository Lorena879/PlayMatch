using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace PlayMatch;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent(); // ← ERA EL ERROR QUE DAÑABA TODO
    }

    public static async Task DisplaySnackbarAsync(string message)
    {
        var snackbar = Snackbar.Make(
            message,
            visualOptions: new SnackbarOptions
            {
                BackgroundColor = Colors.Black,
                TextColor = Colors.White,
                ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(6),
            });

        await snackbar.Show();
    }

    public static async Task DisplayToastAsync(string message)
    {
        var toast = Toast.Make(message, textSize: 16);
        await toast.Show();
    }

    public static async Task LogoutAsync()
    {
        await Shell.Current.GoToAsync("///LoginPage");
    }
}
