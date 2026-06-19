using SubscriptionPro.Views;

namespace SubscriptionPro;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(PaywallPage), typeof(PaywallPage));
        Routing.RegisterRoute(nameof(SuccessPage), typeof(SuccessPage));
        Routing.RegisterRoute(nameof(ManagePage), typeof(ManagePage));

        // Jump straight to a seeded screen when launched for screenshots.
        var route = Bootstrap.Mode switch
        {
            DemoMode.Success => $"//{nameof(SuccessPage)}",
            DemoMode.Manage => $"//{nameof(ManagePage)}",
            _ => null,
        };
        if (route is not null)
            Dispatcher.Dispatch(async () => { await Task.Delay(150); await GoToAsync(route); });
    }
}
