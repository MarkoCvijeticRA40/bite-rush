namespace Application.Helpers;

public static class CurrencyHelper
{
    public static string GetCurrencySymbol(string? currencyCode)
    {
        return currencyCode?.ToUpperInvariant() switch
        {
            "USD" => "$",
            "EUR" => "€",
            _ => currencyCode ?? "?"
        };
    }
}
