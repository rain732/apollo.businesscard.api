namespace Apollo.BusinessCard.Application.Common.Interfaces;

public interface IDateTimeService
{
    DateTime? ConvertStringToDateTime(string value);
    bool IsValidConvertStringToDateTime(string value);
}
