using PlayMatch.Models;

namespace PlayMatch.Services;

public class ProfileService
{
    private readonly List<Profile> _profiles = new()
    {
        new Profile { Id = 1, FullName = "Sofía López", Age = 26, City = "Medellín", Bio = "Amante del café y las caminatas.", PhotoFile="p1.jpg" },
        new Profile { Id = 2, FullName = "Carlos Méndez", Age = 29, City = "Bogotá", Bio = "Músico y viajero.", PhotoFile="p2.jpg" },
        new Profile { Id = 3, FullName = "Valeria Ruiz", Age = 24, City = "Cali", Bio = "Chef aficionada y runner.", PhotoFile="p3.jpg" },
        new Profile { Id = 4, FullName = "Andrés Gómez", Age = 31, City = "Cartagena", Bio = "Amante del mar y la fotografía.", PhotoFile="p4.jpg" }
    };

    public Task<List<Profile>> GetProfilesAsync() => Task.FromResult(_profiles.OrderBy(x => x.Id).ToList());

    // Placeholder for like/match actions
    public Task LikeProfileAsync(int profileId) { /* save to DB later */ return Task.CompletedTask; }
}
