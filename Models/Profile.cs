namespace PlayMatch.Models;

public class Profile
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string City { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string PhotoFile { get; set; } = "profile_placeholder.png"; // place in Resources/Images
}
