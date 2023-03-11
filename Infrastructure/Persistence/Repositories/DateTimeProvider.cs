using Application.Contracts;

namespace Infrastructure.Persistence.Repositories;
internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
