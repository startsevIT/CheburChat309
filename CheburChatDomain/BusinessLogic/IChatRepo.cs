using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IChatRepo
{
    public void Create(Guid UserId);
    List<GetChatDTO> Read(Guid ChatId, Guid UserId);
}
