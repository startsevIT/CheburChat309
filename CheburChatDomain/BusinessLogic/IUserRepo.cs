using Domain.BusinessEntites.DTOs;

namespace Domain.BusinessLogic;

public interface IUserRepo
{
    public void Register(RegisterUserDTO dto);
    public string Login(LoginUserDTO dto);
    public GetUserDTO Read(Guid UserId);
}
