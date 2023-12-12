namespace OutboxPattern.Domain.Enums;

public enum OrderStatus
{
    Preparing = 0,
    Confirmed = 1,
    Declined = 2,
    OnWay = 3,
    Completed = 4
}