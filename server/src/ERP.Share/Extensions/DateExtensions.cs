namespace ERP.Share.Extensions;
public static class DateExtensions
{
    private static readonly string[] validFormats = new[] { "dd/MM/yyyy", "dd/MM/yyyy HH:mm", "dd/MM/yyyy HH:mm:ss" };

    public static DateTime ConvertToDate(this string dateString)
    {
        if (DateTime.TryParseExact(dateString, validFormats, null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date;
        }

        throw new FormatException($"The date string '{dateString}' is not in a recognized format.");
    }


    public static DateOnly ConvertToDateOnly(this string dateString)
    {
        if (DateOnly.TryParseExact(dateString, validFormats, null, System.Globalization.DateTimeStyles.None, out DateOnly result))
        {
            return result;
        }
        else
        {
            throw new FormatException($"The date string '{dateString}' is not in a recognized format.");
        }
    }

    public static bool IsValidDate(string? date)
    {
        if (string.IsNullOrWhiteSpace(date))
        {
            return false;
        }

        return DateTime.TryParseExact(date, validFormats, null, System.Globalization.DateTimeStyles.None, out DateTime tempDate);
    }
}

