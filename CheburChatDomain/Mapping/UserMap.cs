using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;

namespace Domain.Mapping;

public static class UserMap
{
    public static User Map(this RegisterUserDTO dto)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Login = dto.Login,
            NickName = dto.NickName,
            Password = dto.Password,
            Chats = []
        };
    }
    public static GetUserDTO Map(this User user, List<GetInListChatDTO> chats)
    {
        return new GetUserDTO(user.NickName, user.Login, chats);
    }
}
