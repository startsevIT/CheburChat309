using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IUserRepo
{
    public Task RegisterAsync(RegisterUserDTO dto);
    public Task<string> LoginAsync(LoginUserDTO dto);
    public Task<GetUserDTO> ReadAsync(Guid UserId);
}
