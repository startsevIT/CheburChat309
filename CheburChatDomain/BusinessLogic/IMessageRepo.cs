using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IMessageRepo
{
    public void Create(CreateMessageDTO dto, Guid userId);
    public Task<GetMessageDTO> Read(Guid MessageId);
}
