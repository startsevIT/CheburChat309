using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IMessageRepo
{
    Task CreateAsync(CreateMessageDTO dto, Guid userId);
    Task<GetMessageDTO> ReadAsync(Guid MessageId);
}
