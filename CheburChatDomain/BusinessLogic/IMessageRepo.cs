using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IMessageRepo
{
    public void CreateAsync(CreateMessageDTO dto, Guid userId);
    public Task<GetMessageDTO> ReadAsync(Guid MessageId);
}
