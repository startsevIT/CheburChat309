using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IChatRepo
{
    public void CreateAsync(CreateChatDTO dto, Guid UserId);
    Task<GetChatDTO> ReadAsync(Guid ChatId, Guid UserId);
}
