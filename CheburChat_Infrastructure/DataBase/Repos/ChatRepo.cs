using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;
using Domain.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Repos;

public class ChatRepo : IChatRepo
{
    public async void CreateAsync(CreateChatDTO dto, Guid UserId)
    {
        using SqLiteDbContext db = new();
        User user = await db.Users.FindAsync(UserId) 
            ?? throw new Exception("Not found");
        Chat chat = dto.Map(user);
        db.Chats.Add(chat);
        db.SaveChanges();
    }

    //Протестировать и исправить в случае не работы
    public async Task<GetChatDTO> ReadAsync(Guid ChatId, Guid UserId)
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

        List<string> nickNames = [];
        foreach (var u in chat.Users)
            nickNames.Add(u.NickName);

        List<GetMessageDTO> messages = [];
        MessageRepo messageRepo = new ();
        //Заменить на Mapping
        foreach (var m in chat.Messages)
            messages.Add(await messageRepo.ReadAsync(m.Id));
        
        return new(chat.Name, messages, nickNames);
    }
}
