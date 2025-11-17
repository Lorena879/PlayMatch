using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace PlayMatch.Utilities
{
    public static class UIHelpers
    {
        public static async Task DisplayToastAsync(string message)
        {
            var toast = Toast.Make(message, textSize: 16);
            await toast.Show();
        }

        public static async Task DisplaySnackbarAsync(string message)
        {
            var snackbar = Snackbar.Make(message);
            await snackbar.Show();
        }
    }
}
