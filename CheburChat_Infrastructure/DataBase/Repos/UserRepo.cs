using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;
using Domain.Mapping;
using Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.DataBase.Repos;

public class UserRepo : IUserRepo
{
    public async Task<string> LoginAsync(LoginUserDTO dto)
    {
        using SqLiteDbContext db = new ();

        User? user = await db.Users.FirstOrDefaultAsync(x => x.Login == dto.Login) 
            ?? throw new Exception("user not found");

        if (user.Password != dto.Password)
            throw new Exception("Password incorrect");

        List<Claim> claims = [ new Claim("id", user.Id.ToString()) ];

        return AuthOptions.CreateToken(claims);
    }

    public async Task<GetUserDTO> ReadAsync(Guid UserId)
    {
        using SqLiteDbContext db = new ();
        User? user = await db.Users
            .Include(x => x.Chats)
            .FirstAsync(x => x.Id == UserId) 
            ?? throw new Exception("Not Found");

        //Это
        List<GetInListChatDTO> chats = [..user.Chats.Select(x => x.Map())];

        //И это - одно и то же
        //List<GetInListChatDTO> chats = [];
        //foreach (var chat in user.Chats)
        //    chats.Add(chat.Map());

        return user.Map(chats);
    }

    public async void RegisterAsync(RegisterUserDTO dto)
    {
        using SqLiteDbContext db = new ();
        User? tryUser = await db.Users.FirstOrDefaultAsync(x => x.Login == dto.Login);

        if (tryUser != null)
            throw new Exception("Такой пользователь уже есть");

        User user = dto.Map();
        db.Users.Add(user);
        db.SaveChanges();
    }
}
