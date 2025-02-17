using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IUserRepo
{
    Task RegisterAsync(RegisterUserDTO dto);
    Task<string> LoginAsync(LoginUserDTO dto);
    Task<GetUserDTO> ReadAsync(Guid UserId);
}
