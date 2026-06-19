namespace SubscriptionPro.Models;

/// <summary>
/// Billing period for a subscription product. Mirrors the App Store
/// subscription duration / Google Play billing period.
/// </summary>
public enum BillingPeriod
{
    Monthly,
    Annual
}

/// <summary>
/// A purchasable subscription product. In a production build these fields are
/// populated from <c>StoreKit Product</c> (iOS) and
/// <c>BillingClient.queryProductDetailsAsync</c> (Android) rather than hard-coded.
/// </summary>
public record SubscriptionPlan
{
    public required string ProductId { get; init; }
    public required string Title { get; init; }
    public required BillingPeriod Period { get; init; }

    /// <summary>Localized price string exactly as returned by the store (e.g. "$59.99").</summary>
    public required string LocalizedPrice { get; init; }

    /// <summary>Secondary line, e.g. "$4.99/mo" effective rate for an annual plan.</summary>
    public string? EffectiveRate { get; init; }

    /// <summary>Promo badge text, e.g. "SAVE 50%". Null hides the badge.</summary>
    public string? BadgeText { get; init; }

    public decimal RawPrice { get; init; }

    public string PeriodLabel => Period == BillingPeriod.Annual ? "year" : "month";
}

/// <summary>A single value-proposition row shown on the paywall.</summary>
public record PlanFeature(string Icon, string Title, string Subtitle);

/// <summary>
/// The user's current entitlement. Built from validated receipts / current
/// entitlements (StoreKit <c>Transaction.currentEntitlements</c>, Play Billing
/// <c>queryPurchasesAsync</c>).
/// </summary>
public record SubscriptionStatus
{
    public bool IsActive { get; init; }
    public string? ProductId { get; init; }
    public string? PlanTitle { get; init; }
    public string? LocalizedPrice { get; init; }
    public string? BadgeText { get; init; }
    public BillingPeriod Period { get; init; }
    public DateTime? NextBillingDate { get; init; }
    public bool AutoRenewing { get; init; }

    public static SubscriptionStatus None => new() { IsActive = false };
}

/// <summary>Outcome of a purchase or restore attempt.</summary>
public enum PurchaseOutcome
{
    Success,
    UserCancelled,
    AlreadyOwned,
    PaymentInvalid,
    NetworkError,
    ValidationFailed,
    NothingToRestore
}

public record PurchaseResult(PurchaseOutcome Outcome, SubscriptionStatus? Status = null, string? Message = null)
{
    public bool IsSuccess => Outcome == PurchaseOutcome.Success;

    /// <summary>True for outcomes the user silently dismissed (no error UI needed).</summary>
    public bool IsSilent => Outcome == PurchaseOutcome.UserCancelled;
}
