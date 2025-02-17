using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;
using Domain.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Repos;

public class ChatRepo : IChatRepo
{
    public async Task CreateAsync(CreateChatDTO dto, Guid UserId)
    {
        using SqLiteDbContext db = new();

        User user = await db.Users.FindAsync(UserId) 
            ?? throw new Exception("Not found");

        db.Chats.Add(dto.Map(user));
        db.SaveChanges();
    }

    public async Task<GetChatDTO> ReadAndLinkAsync(Guid ChatId, Guid UserId)
    {
        using SqLiteDbContext db = new();

        Chat? chat = await db.Chats
            .Include(x => x.Messages)
            .Include(x => x.Users)
            .FirstAsync(x => x.Id == ChatId) 
            ?? throw new Exception("Not Found");

        if(chat.Users.Find(u => u.Id == UserId) == null)
        {
            User? user = await db.Users.FindAsync(UserId) 
                ?? throw new Exception("Not found");

            chat.Users.Add(user);
            db.SaveChanges();
        }

        //This
        List<string> nickNames = [..chat.Users.Select(x => x.NickName)];

        //And this - equals
        //List<string> nickNames1 = [];
        //foreach (var u in chat.Users)
        //    nickNames1.Add(u.NickName);

        List<GetMessageDTO> messages = [..chat.Messages.Select(x => x.Map(x.User))];
        
        return new(chat.Name, messages, nickNames);
    }
}
