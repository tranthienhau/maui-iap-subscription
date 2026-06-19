using SubscriptionPro.ViewModels;

namespace SubscriptionPro.Views;

public partial class SuccessPage : ContentPage
{
    private readonly SuccessViewModel _vm;

    public SuccessPage(SuccessViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();

        // Subtle entrance + gentle float on the success badge.
        Badge.Scale = 0.9;
        Badge.Opacity = 0;
        await Badge.ScaleTo(1, 500, Easing.SpringOut);
        Badge.FadeTo(1, 300);
        FloatLoop();

        if (Bootstrap.Mode == DemoMode.AutoDemo)
        {
            await Task.Delay(2600);
            await Shell.Current.GoToAsync($"//{nameof(ManagePage)}");
        }
    }

    private async void FloatLoop()
    {
        while (Badge.IsVisible)
        {
            await Badge.TranslateTo(0, -8, 1600, Easing.SinInOut);
            await Badge.TranslateTo(0, 0, 1600, Easing.SinInOut);
        }
    }
}
