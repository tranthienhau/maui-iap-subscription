using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubscriptionPro.Services;
using SubscriptionPro.Views;

namespace SubscriptionPro.ViewModels;

public partial class SuccessViewModel : ObservableObject
{
    private readonly ISubscriptionService _store;

    public SuccessViewModel(ISubscriptionService store)
    {
        _store = store;
    }

    [ObservableProperty] private string _planTitle = "Premium";
    [ObservableProperty] private string _price = "";
    [ObservableProperty] private string? _badgeText;
    [ObservableProperty] private bool _hasBadge;
    [ObservableProperty] private string _nextBillingDate = "";

    public ObservableCollection<string> Perks { get; } = new()
    {
        "Unlimited Global Access",
        "Advanced Performance Insights",
        "Priority 24/7 Support",
    };

    public async Task LoadAsync()
    {
        var s = await _store.GetStatusAsync();
        PlanTitle = s.PlanTitle ?? "Premium";
        Price = s.LocalizedPrice ?? "";
        BadgeText = s.BadgeText;
        HasBadge = !string.IsNullOrEmpty(s.BadgeText);
        NextBillingDate = s.NextBillingDate?.ToString("MMM d, yyyy") ?? "-";
    }

    [RelayCommand]
    private Task StartExploringAsync() => Shell.Current.GoToAsync($"//{nameof(ManagePage)}");

    [RelayCommand]
    private Task ManageAsync() => Shell.Current.GoToAsync($"//{nameof(ManagePage)}");
}
