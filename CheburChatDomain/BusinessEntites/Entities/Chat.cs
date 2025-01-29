namespace Domain.BusinessEntites.Entities;

public class Chat(string name, Guid userId)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public List<Guid> UserIds { get; set; } = [userId];
    public List<Guid> MessageIds { get; set; } = [];
}
