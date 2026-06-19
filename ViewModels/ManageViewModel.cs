using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubscriptionPro.Models;
using SubscriptionPro.Services;
using SubscriptionPro.Views;

namespace SubscriptionPro.ViewModels;

/// <summary>
/// Active-subscription management: shows the current entitlement, renewal
/// state, and lets the user cancel auto-renew or restore.
/// </summary>
public partial class ManageViewModel : ObservableObject
{
    private readonly ISubscriptionService _store;

    public ManageViewModel(ISubscriptionService store)
    {
        _store = store;
    }

    [ObservableProperty] private bool _isActive;
    [ObservableProperty] private string _planTitle = "";
    [ObservableProperty] private string _price = "";
    [ObservableProperty] private string _nextBillingDate = "";
    [ObservableProperty] private string _renewalStatus = "";
    [ObservableProperty] private bool _autoRenewing;
    [ObservableProperty] private bool _isBusy;

    public async Task LoadAsync()
    {
        var s = await _store.GetStatusAsync();
        IsActive = s.IsActive;
        PlanTitle = s.PlanTitle ?? "No active plan";
        Price = s.LocalizedPrice ?? "";
        AutoRenewing = s.AutoRenewing;
        NextBillingDate = s.NextBillingDate?.ToString("MMM d, yyyy") ?? "-";
        RenewalStatus = s.AutoRenewing
            ? $"Renews automatically on {NextBillingDate}"
            : $"Access ends on {NextBillingDate}";
    }

    [RelayCommand]
    private async Task CancelRenewalAsync()
    {
        if (IsBusy || !AutoRenewing)
            return;

        bool ok = await Shell.Current.DisplayAlert("Cancel auto-renew?",
            "You'll keep premium until the end of the current billing period.",
            "Cancel renewal", "Keep plan");
        if (!ok)
            return;

        IsBusy = true;
        await _store.CancelAutoRenewAsync();
        IsBusy = false;
        await LoadAsync();
    }

    [RelayCommand]
    private async Task RestoreAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;
        var result = await _store.RestoreAsync();
        IsBusy = false;

        await Shell.Current.DisplayAlert("Restore",
            result.IsSuccess ? "Your subscription has been restored."
                             : result.Message ?? "Nothing to restore.", "OK");
        await LoadAsync();
    }

    [RelayCommand]
    private Task ChangePlanAsync() => Shell.Current.GoToAsync($"//{nameof(PaywallPage)}");
}
