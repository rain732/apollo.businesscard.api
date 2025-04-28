using Apollo.BusinessCard.Application.Common.Interfaces;

namespace Apollo.BusinessCard.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime? ConvertStringToDateTime(string value)
    {
        DateTime dateTime;
        if (!DateTime.TryParse(value, out dateTime))
        {
            return null;
        }
        return dateTime;
    }

    public bool IsValidConvertStringToDateTime(string value)
    {
        DateTime date;
        if (DateTime.TryParse(value, out date))
            return true;
        return false;
    }
}
