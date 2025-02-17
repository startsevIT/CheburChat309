using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IChatRepo
{
    Task CreateAsync(CreateChatDTO dto, Guid UserId);
    Task<GetChatDTO> ReadAndLinkAsync(Guid ChatId, Guid UserId);
}
