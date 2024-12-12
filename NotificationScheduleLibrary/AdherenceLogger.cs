namespace NotificationScheduleLibrary;

public class AdherenceLogger
{
    private readonly List<AdherenceLog> _adherenceLogs = new List<AdherenceLog>(); //This could also be a database

    public void LogAdherence(int reminderId, AdherenceStatus status)
    {
        _adherenceLogs.Add(new AdherenceLog
        {
            ReminderId = reminderId,
            Status = status,
            Timestamp = DateTime.Now
        });
    }

    public List<AdherenceLog> GetAdherenceLogs()
    {
        return _adherenceLogs;
    }
}

public class AdherenceLog
{
    public int ReminderId { get; init; }
    public AdherenceStatus Status { get; init; }
    public DateTime Timestamp { get; init; }
}

public enum AdherenceStatus
{
    Taken,
    Missed,
    Rescheduled
}
