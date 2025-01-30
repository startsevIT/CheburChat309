namespace Domain.BusinessEntites.Entities;

public class Message(Guid id, string text, User user, DateTime dateTime)
{
    public Guid Id { get; set; } = id;
    public string Text { get; set; } = text;
    public User User { get; set; } = user;
    public DateTime DateTime { get; set; } = dateTime;
    //crutch
    public Message() : this("",new ()) { }
    public Message(string text, User user) 
        : this(Guid.NewGuid(), text, user, DateTime.Now) {}
}
