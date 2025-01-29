using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;

namespace Infrastructure.DataBase.Repos;

public class MessageRepo : IMessageRepo
{
    public void Create(CreateMessageDTO dto, Guid userId)
    {
        Message message = new(dto.Text, userId);
        using SqLiteDbContext db = new();
        db.Messages.Add(message);
        db.SaveChanges();
    }

    public async Task<GetMessageDTO> Read(Guid MessageId)
    {
        using SqLiteDbContext db = new();
        Message? message = await db.Messages.FindAsync(MessageId) 
            ?? throw new Exception("Not Found message");
        User? user = await db.Users.FindAsync(message.UserId) 
            ?? throw new Exception("Not Found user");

        return new(user.NickName, message.Text, message.DateTime);
    }
}
