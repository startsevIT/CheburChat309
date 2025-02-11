using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;

namespace Domain.Mapping;

public static class ChatMap
{
    public static Chat Map(this CreateChatDTO dto, User user)
    {
        return new Chat
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Users = [user],
            Messages = []
        };
    }
    public static GetChatDTO Map(this Chat chat, List<GetMessageDTO> messages, List<string> nickNames)
    {
        return new GetChatDTO(chat.Name, messages, nickNames);
    }
    public static GetInListChatDTO Map(this Chat chat)
    {
        return new GetInListChatDTO(chat.Name, chat.Id);
    }
}
