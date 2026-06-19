using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SubscriptionPro.Services;
using SubscriptionPro.ViewModels;
using SubscriptionPro.Views;

namespace SubscriptionPro;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("HankenGrotesk-Regular.ttf", "Hanken");
                fonts.AddFont("HankenGrotesk-Medium.ttf", "HankenMedium");
                fonts.AddFont("HankenGrotesk-SemiBold.ttf", "HankenSemiBold");
                fonts.AddFont("HankenGrotesk-Bold.ttf", "HankenBold");
            });

        // One store service for the whole app session.
        builder.Services.AddSingleton<ISubscriptionService, MockSubscriptionService>();

        builder.Services.AddSingleton<PaywallViewModel>();
        builder.Services.AddSingleton<SuccessViewModel>();
        builder.Services.AddSingleton<ManageViewModel>();

        builder.Services.AddSingleton<PaywallPage>();
        builder.Services.AddSingleton<SuccessPage>();
        builder.Services.AddSingleton<ManagePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
