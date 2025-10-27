namespace Domain.Users;

public class UserRegisteredEvent
{
    public string EventType { get; set; }
    public Guid UserId { get; set; }
    public DateTime Timestamp { get; set; }
}
