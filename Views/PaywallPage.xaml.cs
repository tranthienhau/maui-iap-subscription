using SubscriptionPro.Models;
using SubscriptionPro.ViewModels;

namespace SubscriptionPro.Views;

public partial class PaywallPage : ContentPage
{
    private readonly PaywallViewModel _vm;
    private bool _demoRan;

    public PaywallPage(PaywallViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_vm.Plans.Count == 0)
            await _vm.LoadAsync();

        if (_demoRan)
            return;
        _demoRan = true;

        if (Bootstrap.Mode == DemoMode.Error)
            await RunErrorDemoAsync();
        else if (Bootstrap.Mode == DemoMode.AutoDemo)
            await RunAutoDemoAsync();
    }

    // Forces the failure path so the error dialog is captured.
    private async Task RunErrorDemoAsync()
    {
        await Task.Delay(600);
        _vm.SimulateFailure = true;
        await _vm.ContinueCommand.ExecuteAsync(null);
    }

    // Walks the happy path on its own for the demo recording.
    private async Task RunAutoDemoAsync()
    {
        var monthly = _vm.Plans.FirstOrDefault(p => p.Period == BillingPeriod.Monthly);
        var annual = _vm.Plans.FirstOrDefault(p => p.Period == BillingPeriod.Annual);

        await Task.Delay(1200);
        if (monthly is not null) _vm.SelectPlanCommand.Execute(monthly);
        await Task.Delay(1000);
        if (annual is not null) _vm.SelectPlanCommand.Execute(annual);
        await Task.Delay(1000);
        await _vm.ContinueCommand.ExecuteAsync(null); // navigates to SuccessPage
    }
}
