namespace Domain.BusinessEntites.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public User User { get; set; }
    public DateTime DateTime { get; set; }
}
