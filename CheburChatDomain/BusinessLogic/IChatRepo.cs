using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IChatRepo
{
    public void Create(CreateChatDTO dto, Guid UserId);
    Task<GetChatDTO> Read(Guid ChatId, Guid UserId);
}
