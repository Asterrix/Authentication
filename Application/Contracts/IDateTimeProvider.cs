namespace Application.Contracts;
public interface IDateTimeProvider
{
    DateTime UtcNow => DateTime.UtcNow;
}
