namespace Domain.BusinessEntites.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateTime { get; set; }
}
