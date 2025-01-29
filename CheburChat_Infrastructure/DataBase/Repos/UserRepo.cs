using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Repos;

public class UserRepo : IUserRepo
{
    public string Login(LoginUserDTO dto)
    {
        throw new NotImplementedException();
    }

    public GetUserDTO Read(Guid UserId)
    {
        throw new NotImplementedException();
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
