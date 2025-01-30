namespace Domain.BusinessEntites.Entities;

public class User(Guid id, string login, string nickName, string password, List<Chat> chats)
{
    public Guid Id { get; set; } = id;
    public string Login { get; set; } = login;
    public string NickName { get; set; } = nickName;
    public string Password { get; set; } = password;
    public List<Chat> Chats { get; set; } = chats;
    //crutch
    public User() : this("","","") { }
    public User(string login, string nickName, string password) : 
        this(Guid.NewGuid(), login, nickName, password, []) {}
}
