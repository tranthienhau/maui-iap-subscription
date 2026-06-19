using SubscriptionPro.Models;

namespace SubscriptionPro.Services;

/// <summary>
/// In-memory stand-in for StoreKit 2 / Google Play Billing. It mimics the real
/// timing and state machine of a purchase (load products -> buy -> validate
/// receipt -> grant entitlement -> restore / cancel) so the screens behave
/// exactly as they would against a live store, minus the network and a real
/// store account.
/// </summary>
public sealed class MockSubscriptionService : ISubscriptionService
{
    private SubscriptionStatus _status = SubscriptionStatus.None;

    public MockSubscriptionService()
    {
        // For the Success / Manage demo entry points, start with an active
        // annual subscription so those screens render real entitlement data.
        if (Bootstrap.SeedActive)
        {
            _status = new SubscriptionStatus
            {
                IsActive = true,
                ProductId = "com.subscriptionpro.premium.annual",
                PlanTitle = "Annual Premium",
                LocalizedPrice = "$59.99/yr",
                BadgeText = "SAVE 50%",
                Period = BillingPeriod.Annual,
                NextBillingDate = DateTime.Today.AddYears(1),
                AutoRenewing = true,
            };
        }
    }

    public PurchaseOutcome? FailNextPurchaseWith { get; set; }

    public IReadOnlyList<PlanFeature> Features { get; } = new List<PlanFeature>
    {
        new("ad", "Ad-free Experience", "Focus entirely on your content without interruptions."),
        new("bolt", "Offline Access", "Download and enjoy your media anytime, anywhere."),
        new("hq", "Lossless Quality", "Crystal clear high-fidelity audio and 4K visual streaming."),
    };

    private readonly IReadOnlyList<SubscriptionPlan> _plans = new List<SubscriptionPlan>
    {
        new()
        {
            ProductId = "com.subscriptionpro.premium.monthly",
            Title = "Monthly",
            Period = BillingPeriod.Monthly,
            LocalizedPrice = "$9.99",
            RawPrice = 9.99m,
        },
        new()
        {
            ProductId = "com.subscriptionpro.premium.annual",
            Title = "Annual",
            Period = BillingPeriod.Annual,
            LocalizedPrice = "$59.99",
            EffectiveRate = "$4.99/mo",
            BadgeText = "SAVE 50%",
            RawPrice = 59.99m,
        },
    };

    public async Task<IReadOnlyList<SubscriptionPlan>> GetPlansAsync()
    {
        // Simulate the round-trip to the store catalog.
        await Task.Delay(450);
        return _plans;
    }

    public Task<SubscriptionStatus> GetStatusAsync() => Task.FromResult(_status);

    public async Task<PurchaseResult> PurchaseAsync(SubscriptionPlan plan)
    {
        // Step 1: payment sheet + processing.
        await Task.Delay(1400);

        if (FailNextPurchaseWith is { } forced)
        {
            FailNextPurchaseWith = null;
            return new PurchaseResult(forced, Message: MessageFor(forced));
        }

        // Step 2: receipt validation before granting the entitlement.
        await Task.Delay(700);

        _status = new SubscriptionStatus
        {
            IsActive = true,
            ProductId = plan.ProductId,
            PlanTitle = $"{plan.Title} Premium",
            LocalizedPrice = $"{plan.LocalizedPrice}/{(plan.Period == BillingPeriod.Annual ? "yr" : "mo")}",
            BadgeText = plan.BadgeText,
            Period = plan.Period,
            NextBillingDate = plan.Period == BillingPeriod.Annual
                ? DateTime.Today.AddYears(1)
                : DateTime.Today.AddMonths(1),
            AutoRenewing = true,
        };

        return new PurchaseResult(PurchaseOutcome.Success, _status);
    }

    public async Task<PurchaseResult> RestoreAsync()
    {
        await Task.Delay(1100);

        if (_status.IsActive)
            return new PurchaseResult(PurchaseOutcome.Success, _status);

        return new PurchaseResult(PurchaseOutcome.NothingToRestore,
            Message: "No previous purchases were found for this account.");
    }

    public async Task<SubscriptionStatus> CancelAutoRenewAsync()
    {
        await Task.Delay(600);
        if (_status.IsActive)
            _status = _status with { AutoRenewing = false };
        return _status;
    }

    private static string MessageFor(PurchaseOutcome outcome) => outcome switch
    {
        PurchaseOutcome.PaymentInvalid => "Your payment method was declined. Update it in Settings and try again.",
        PurchaseOutcome.NetworkError => "We couldn't reach the store. Check your connection and try again.",
        PurchaseOutcome.ValidationFailed => "We couldn't verify your purchase. You have not been charged.",
        PurchaseOutcome.AlreadyOwned => "You already own this subscription. Try Restore instead.",
        _ => "Something went wrong. Please try again.",
    };
}
