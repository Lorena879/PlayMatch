using PlayMatch.Models;
using PlayMatch.Services;

namespace PlayMatch.Views;

public partial class HomePage : ContentPage
{
    private readonly ProfileService _profileService = new();
    private List<Profile> _profiles = new();
    private readonly List<Frame> _cardFrames = new();

    public HomePage()
    {
        InitializeComponent();
        LoadProfiles();
    }

    private async void LoadProfiles()
    {
        _profiles = await _profileService.GetProfilesAsync();
        RenderCards();
    }

    private void RenderCards()
    {
        CardsGrid.Children.Clear();
        _cardFrames.Clear();

        for (int i = _profiles.Count - 1; i >= 0; i--)
        {
            var card = CreateCard(_profiles[i]);
            _cardFrames.Add(card);
            CardsGrid.Children.Add(card);
        }
    }

    private Frame CreateCard(Profile p)
    {
        var img = new Image
        {
            Source = p.PhotoFile,
            Aspect = Aspect.AspectFill,
            HeightRequest = 420
        };

        var name = new Label
        {
            Text = $"{p.FullName}, {p.Age}",
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White
        };

        var city = new Label
        {
            Text = p.City,
            FontSize = 14,
            TextColor = Colors.White
        };

        var bio = new Label
        {
            Text = p.Bio,
            FontSize = 14,
            TextColor = Colors.White
        };

        var stack = new VerticalStackLayout
        {
            Padding = 12,
            Spacing = 8,
            Children = { img, name, city, bio }
        };

        var frame = new Frame
        {
            Content = stack,
            CornerRadius = 20,
            Margin = 10,
            Padding = 0,
            BackgroundColor = Color.FromArgb("#22FFFFFF"), // efecto vidrio
            Shadow = new Shadow
            {
                Brush = Brush.Black,
                Offset = new Point(0, 8),
                Radius = 20,
                Opacity = 0.25f
            }
        };

        // gesto pan
        var pan = new PanGestureRecognizer();
        double startX = 0;

        pan.PanUpdated += (s, e) =>
        {
            if (e.StatusType == GestureStatus.Started)
                startX = frame.TranslationX;

            else if (e.StatusType == GestureStatus.Running)
            {
                frame.TranslationX = startX + e.TotalX;
                frame.Rotation = frame.TranslationX / 25;
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                if (frame.TranslationX > 120)
                    DoLike(frame, p);
                else if (frame.TranslationX < -120)
                    DoNope(frame, p);
                else
                {
                    frame.TranslateTo(0, 0, 150, Easing.SpringOut);
                    frame.RotateTo(0, 150, Easing.SpringOut);
                }
            }
        };

        frame.GestureRecognizers.Add(pan);

        return frame;
    }


    private async void DoLike(Frame frame, Profile p)
    {
        await frame.TranslateTo(800, 0, 250);
        await _profileService.LikeProfileAsync(p.Id);

        RemoveTopCard(frame);

        await DisplayAlert("Like", $"Te gusta {p.FullName}", "OK");
    }

    private async void DoNope(Frame frame, Profile p)
    {
        await frame.TranslateTo(-800, 0, 250);
        RemoveTopCard(frame);
    }

    private void RemoveTopCard(Frame frame)
    {
        CardsGrid.Children.Remove(frame);
        _cardFrames.Remove(frame);

        if (_profiles.Count > 0)
            _profiles.RemoveAt(_profiles.Count - 1);
    }

    private void BtnNope_Clicked(object sender, EventArgs e)
    {
        if (_cardFrames.Any())
            DoNope(_cardFrames.Last(), _profiles.Last());
    }

    private void BtnSuper_Clicked(object sender, EventArgs e)
    {
        if (_cardFrames.Any())
            DoLike(_cardFrames.Last(), _profiles.Last());
    }

    private void BtnLike_Clicked(object sender, EventArgs e)
    {
        if (_cardFrames.Any())
            DoLike(_cardFrames.Last(), _profiles.Last());
    }
}
