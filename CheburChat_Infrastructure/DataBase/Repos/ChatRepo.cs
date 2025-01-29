using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Repos;

public class ChatRepo : IChatRepo
{
    public void Create(CreateChatDTO dto, Guid UserId)
    {
        Chat chat = new(dto.Name, UserId);
        using SqLiteDbContext db = new ();
        db.Chats.Add(chat);
        db.SaveChanges();
    }

    public async Task<GetChatDTO> Read(Guid ChatId, Guid UserId)
    {
        using SqLiteDbContext db = new();
        Chat? chat = await db.Chats
            .Include(x => x.MessageIds)
            .Include(x => x.UserIds)
            .FirstAsync(x => x.Id == ChatId) 
            ?? throw new Exception("Not Found");

        if(!chat.UserIds.Contains(UserId))
            chat.UserIds.Add(UserId);

        List<string> nickNames = [];
        foreach (var u in chat.UserIds)
            nickNames.Add(db.Users.Find(u).NickName);

        List<GetMessageDTO> messages = new();
        MessageRepo messageRepo = new ();

        foreach (var mId in chat.MessageIds)
            messages.Add(await messageRepo.Read(mId));


        return new(chat.Name, messages, nickNames);
    }
}
