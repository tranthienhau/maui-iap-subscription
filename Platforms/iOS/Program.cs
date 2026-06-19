using ObjCRuntime;
using UIKit;

namespace SubscriptionPro;

public class Program
{
    // This is the main entry point of the application.
    static void Main(string[] args)
    {
        Bootstrap.Parse(args);
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}
