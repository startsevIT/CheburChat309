using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;
using Domain.Mapping;
using Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.DataBase.Repos;

public class UserRepo : IUserRepo
{
    public async Task<string> LoginAsync(LoginUserDTO dto)
    {
        using SqLiteDbContext db = new ();

        User? user = await db.Users.FirstOrDefaultAsync(x => x.Login == dto.Login) 
            ?? throw new Exception("user not found");

        if (AuthOptions.VerifyPassword(dto.Password,user.Password))
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

        List<GetInListChatDTO> chats = [..user.Chats.Select(x => x.Map())];

        return user.Map(chats);
    }

    public async Task RegisterAsync(RegisterUserDTO dto)
    {
        using SqLiteDbContext db = new ();

        User? tryUser = await db.Users.FirstOrDefaultAsync(x => x.Login == dto.Login);

        if (tryUser != null)
            throw new Exception("User already exist");

        RegisterUserDTO final = new(dto.NickName, dto.Login, AuthOptions.HashPassword(dto.Password));

        db.Users.Add(final.Map());
        db.SaveChanges();
    }
}
