using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;
using Domain.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Repos;

public class MessageRepo : IMessageRepo
{
    public async Task<Guid> CreateAndReturnIdAsync(CreateMessageDTO dto, Guid userId, Guid chatId)
    {
        using SqLiteDbContext db = new();
        User? user = await db.Users.FindAsync(userId) 
            ?? throw new Exception("Не найден пользователь");
        Chat? chat = await db.Chats.FindAsync(chatId)
            ?? throw new Exception("Не найден чат");

        Message message = dto.Map(user, chat);
        await db.Messages.AddAsync(message);
        await db.SaveChangesAsync();
        return message.Id;
    }

    public async Task<ReadMessageDTO> ReadAsync(Guid MessageId)
    {
        using SqLiteDbContext db = new();
        Message? message = await db.Messages
            .Include(x => x.User)
            .FirstAsync(x => x.Id == MessageId) 
            ?? throw new Exception("Not Found message");

        return message.Map(message.User);
    }
}
