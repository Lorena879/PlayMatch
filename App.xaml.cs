using System;
using System.IO;
using Microsoft.Maui.Storage;
using PlayMatch.Services;
using PlayMatch.Models;
using PlayMatch.Views;

namespace PlayMatch;

public partial class App : Application
{
    public static DatabaseService Database { get; private set; }
    public static string DatabasePath { get; private set; }

    public App()
    {
#if WINDOWS || ANDROID || IOS || MACCATALYST
        // Elimine o comente la siguiente línea, ya que InitializeComponent no existe en App.xaml.cs
        // this.InitializeComponent();
#endif

        try
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "playmatch.db3");
            DatabasePath = dbPath;
            Database = new DatabaseService(dbPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR al crear la base de datos: " + ex.Message);
        }

        MainPage = new AppShell();

        // Registrar rutas (útiles para Shell navigation)
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
    }

    protected override async void OnStart()
    {
        base.OnStart();

        try
        {
            if (Database != null)
            {
                var existingUser = await Database.GetUserAsync("cuentaplaymatch@gmail.com", "usuariomatch");
                if (existingUser == null)
                {
                    await Database.AddUserAsync(new User
                    {
                        FullName = "Bienvenido a la App",
                        Email = "cuentaplaymatch@gmail.com",
                        Password = "usuariomatch",
                        CreatedAt = DateTime.Now
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR al crear usuario de prueba: " + ex.Message);
        }
    }
}
