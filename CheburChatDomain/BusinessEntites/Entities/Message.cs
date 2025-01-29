namespace Domain.BusinessEntites.Entities;

public class Message(string text, Guid userId)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = text;
    public Guid UserId { get; set; } = userId;
    public DateTime DateTime { get; set; } = DateTime.Now;
}
