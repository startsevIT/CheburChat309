using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;

namespace Domain.Mapping;

public static class MessageMap
{
    public static Message Map(this CreateMessageDTO dto, User user, Chat chat)
    {
        return new Message()
        {
            Id = Guid.NewGuid(),
            Text = dto.Text,
            User = user,
            Chat = chat,
            DateTime = DateTime.Now
        };
    }
    public static ReadMessageDTO Map(this Message message, User user)
    {
        return new ReadMessageDTO(user.NickName, message.Text, message.DateTime);
    }

}
