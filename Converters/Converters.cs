using System.Globalization;

namespace SubscriptionPro.Converters;

public sealed class InvertBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b ? !b : value!;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b ? !b : value!;
}

public sealed class NotNullConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is not null && !(value is string s && string.IsNullOrEmpty(s));

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

/// <summary>Returns true when the two bound values are equal (item == selected).</summary>
public sealed class EqualityMultiConverter : IMultiValueConverter
{
    public object Convert(object[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is null || values.Length < 2 || values[0] is null || values[1] is null)
            return false;
        return Equals(values[0], values[1]);
    }

    public object[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}

/// <summary>Primary brush when item == selected, otherwise the outline brush.</summary>
public sealed class SelectedToStrokeConverter : IMultiValueConverter
{
    private static readonly Color Primary = Color.FromArgb("#0050cb");
    private static readonly Color Outline = Color.FromArgb("#c2c6d8");

    public object Convert(object[]? values, Type targetType, object? parameter, CultureInfo culture)
        => IsSelected(values) ? Primary : Outline;

    public object[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();

    internal static bool IsSelected(object[]? v)
        => v is { Length: >= 2 } && v[0] is not null && v[1] is not null && Equals(v[0], v[1]);
}

/// <summary>2px border when selected, 1px otherwise.</summary>
public sealed class SelectedToThicknessConverter : IMultiValueConverter
{
    public object Convert(object[]? values, Type targetType, object? parameter, CultureInfo culture)
        => SelectedToStrokeConverter.IsSelected(values) ? 2.0 : 1.0;

    public object[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
