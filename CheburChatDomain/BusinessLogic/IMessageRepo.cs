using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IMessageRepo
{
    Task<Guid> CreateAndReturnIdAsync(CreateMessageDTO dto, Guid userId, Guid chatId);
    Task<ReadMessageDTO> ReadAsync(Guid MessageId);
}
