namespace Domain.BusinessEntites.Entities;

public class User(string login, string nickName, string password)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Login { get; set; } = login;
    public string NickName { get; set; } = nickName;
    public string Password { get; set; } = password;
    public List<Guid> ChatIds { get; set; } = [];
}
