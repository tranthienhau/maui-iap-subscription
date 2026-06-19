namespace SubscriptionPro;

/// <summary>
/// Demo entry mode, selected from a launch argument so screens can be reached
/// deterministically for screenshots / the autoplay demo without a UI driver.
/// Pass via: <c>xcrun simctl launch booted com.subscriptionpro.premium &lt;mode&gt;</c>.
/// Has no effect on a normal launch (defaults to the paywall).
/// </summary>
public enum DemoMode
{
    Paywall,
    Success,
    Manage,
    Error,
    AutoDemo
}

public static class Bootstrap
{
    public static DemoMode Mode { get; private set; } = DemoMode.Paywall;

    /// <summary>Modes that should start with an active subscription seeded.</summary>
    public static bool SeedActive => Mode is DemoMode.Success or DemoMode.Manage;

    public static void Parse(string[]? args)
    {
        // Env var (SIMCTL_CHILD_DEMO_MODE=...) is the reliable channel on the
        // iOS simulator; fall back to command-line args otherwise.
        var candidates = new List<string>();
        var env = Environment.GetEnvironmentVariable("DEMO_MODE");
        if (!string.IsNullOrEmpty(env))
            candidates.Add(env);
        if (args is not null)
            candidates.AddRange(args);
        candidates.AddRange(Environment.GetCommandLineArgs());

        foreach (var raw in candidates)
        {
            switch (raw.Trim().ToLowerInvariant())
            {
                case "success": Mode = DemoMode.Success; return;
                case "manage": Mode = DemoMode.Manage; return;
                case "error": Mode = DemoMode.Error; return;
                case "demo": Mode = DemoMode.AutoDemo; return;
                case "paywall": Mode = DemoMode.Paywall; return;
            }
        }
    }
}
