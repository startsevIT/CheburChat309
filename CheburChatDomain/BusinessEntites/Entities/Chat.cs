namespace Domain.BusinessEntites.Entities;

public class Chat(Guid id, string name, List<User> users, List<Message> messages)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public List<User> Users { get; set; } = users;
    public List<Message> Messages { get; set; } = messages;
    //crutch
    public Chat() : this("",new()) { }
    public Chat(string name, User user) 
        : this(Guid.NewGuid(), name, [user], []) { }
}
