using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Repos;

public class UserRepo : IUserRepo
{
    public Task<string> Login(LoginUserDTO dto)
    {
        throw new NotImplementedException();
    }

    public async Task<GetUserDTO> Read(Guid UserId)
    {
        using SqLiteDbContext db = new ();
        User? user = await db.Users
            .Include(x => x.ChatIds)
            .FirstAsync(x => x.Id == UserId) 
            ?? throw new Exception("Not Found");

        List<GetInListChatDTO> chats = [];
        foreach (var id in user.ChatIds)
        {
            Chat chat = await db.Chats.FindAsync(id) 
                ?? throw new Exception("Not Found");
            chats.Add(new(chat.Name, chat.Id));
        }

        return new GetUserDTO(user.NickName, user.Login,chats);
    }

    public async void Register(RegisterUserDTO dto)
    {
        using SqLiteDbContext db = new ();
        User? tryUser = await db.Users.FirstAsync(x => x.Login == dto.Login);

        if (tryUser != null)
            throw new Exception("Такой пользователь уже есть");

        User user = new(dto.Login,dto.NickName,dto.Password);
        db.Users.Add(user);
        db.SaveChanges();
    }
}
