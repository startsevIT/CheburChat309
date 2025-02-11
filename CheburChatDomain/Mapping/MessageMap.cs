using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;

namespace Domain.Mapping;

public static class MessageMap
{
    public static Message Map(this CreateMessageDTO dto, User user)
    {
        return new Message()
        {
            Id = Guid.NewGuid(),
            Text = dto.Text,
            User = user,
            DateTime = DateTime.Now
        };
    }
    public static GetMessageDTO Map(this Message message, User user)
    {
        return new GetMessageDTO(user.NickName, message.Text, message.DateTime);
    }

}
