using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IMessageRepo
{
    public void Create(CreateMessageDTO dto);
    public GetMessageDTO Read(Guid MessageId);
}
