using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubscriptionPro.Models;
using SubscriptionPro.Services;
using SubscriptionPro.Views;

namespace SubscriptionPro.ViewModels;

public partial class PaywallViewModel : ObservableObject
{
    private readonly ISubscriptionService _store;

    public PaywallViewModel(ISubscriptionService store)
    {
        _store = store;
        foreach (var f in store.Features)
            Features.Add(f);
    }

    public ObservableCollection<PlanFeature> Features { get; } = new();
    public ObservableCollection<SubscriptionPlan> Plans { get; } = new();

    [ObservableProperty]
    private SubscriptionPlan? _selectedPlan;

    [ObservableProperty]
    private bool _isLoadingPlans = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotProcessing))]
    private bool _isProcessing;

    [ObservableProperty]
    private string _processingLabel = "Processing...";

    public bool IsNotProcessing => !IsProcessing;

    // Demo toggle: when on, the next purchase is forced to fail so the
    // error-handling path is visible on a simulator.
    [ObservableProperty]
    private bool _simulateFailure;

    public async Task LoadAsync()
    {
        IsLoadingPlans = true;
        Plans.Clear();
        var plans = await _store.GetPlansAsync();
        foreach (var p in plans)
            Plans.Add(p);

        // Default to the highlighted "best value" annual plan.
        SelectedPlan = Plans.FirstOrDefault(p => p.Period == BillingPeriod.Annual) ?? Plans.FirstOrDefault();
        IsLoadingPlans = false;
    }

    [RelayCommand]
    private void SelectPlan(SubscriptionPlan plan)
    {
        SelectedPlan = plan;
#if IOS || ANDROID
        try { HapticFeedback.Default.Perform(HapticFeedbackType.Click); } catch { }
#endif
    }

    [RelayCommand]
    private async Task ContinueAsync()
    {
        if (SelectedPlan is null || IsProcessing)
            return;

        _store.FailNextPurchaseWith = SimulateFailure ? PurchaseOutcome.PaymentInvalid : null;

        IsProcessing = true;
        ProcessingLabel = "Contacting store...";
        await Task.Delay(500);
        ProcessingLabel = "Validating receipt...";

        var result = await _store.PurchaseAsync(SelectedPlan);
        IsProcessing = false;

        if (result.IsSuccess)
        {
            await Shell.Current.GoToAsync($"//{nameof(SuccessPage)}");
            return;
        }

        if (result.IsSilent)
            return;

        await Shell.Current.DisplayAlert("Purchase failed",
            result.Message ?? "Please try again.", "OK");
    }

    [RelayCommand]
    private async Task RestoreAsync()
    {
        if (IsProcessing)
            return;

        IsProcessing = true;
        ProcessingLabel = "Restoring purchases...";
        var result = await _store.RestoreAsync();
        IsProcessing = false;

        if (result.IsSuccess)
        {
            await Shell.Current.GoToAsync($"//{nameof(SuccessPage)}");
            return;
        }

        await Shell.Current.DisplayAlert("Restore",
            result.Message ?? "Nothing to restore.", "OK");
    }
}
