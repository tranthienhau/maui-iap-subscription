using SubscriptionPro.Models;

namespace SubscriptionPro.Services;

/// <summary>
/// Store-agnostic contract for the subscription purchase flow. A production
/// app provides one implementation backed by StoreKit 2 on iOS and Google Play
/// Billing on Android (behind <c>#if IOS / #if ANDROID</c> or a DI swap); this
/// POC ships <see cref="MockSubscriptionService"/> so the full flow is demoable
/// on a simulator with no store account.
/// </summary>
public interface ISubscriptionService
{
    /// <summary>Marketing value props rendered on the paywall.</summary>
    IReadOnlyList<PlanFeature> Features { get; }

    /// <summary>
    /// Fetch the configured products from the store. Maps to
    /// <c>Product.products(for:)</c> (StoreKit) / <c>queryProductDetailsAsync</c> (Play).
    /// </summary>
    Task<IReadOnlyList<SubscriptionPlan>> GetPlansAsync();

    /// <summary>Current entitlement, recomputed from validated receipts.</summary>
    Task<SubscriptionStatus> GetStatusAsync();

    /// <summary>
    /// Run the full purchase: present the store sheet, take payment, then
    /// server/local-validate the receipt before granting the entitlement.
    /// </summary>
    Task<PurchaseResult> PurchaseAsync(SubscriptionPlan plan);

    /// <summary>Restore previously bought subscriptions (required on iOS).</summary>
    Task<PurchaseResult> RestoreAsync();

    /// <summary>Cancel auto-renew for the active subscription (management).</summary>
    Task<SubscriptionStatus> CancelAutoRenewAsync();

    /// <summary>
    /// Demo hook: force the next purchase to fail with a given outcome so the
    /// error-handling UI can be exercised on a simulator.
    /// </summary>
    PurchaseOutcome? FailNextPurchaseWith { get; set; }
}
