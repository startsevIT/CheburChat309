using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IUserRepo
{
    public void Register(RegisterUserDTO dto);
    public Task<string> Login(LoginUserDTO dto);
    public Task<GetUserDTO> Read(Guid UserId);
}
