namespace Domain.BusinessEntites.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Guid> UserIds { get; set; }
    public List<Guid> MessageIds { get; set; }
}
