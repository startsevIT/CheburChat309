namespace Domain.BusinessEntites.DTOs;

public record class RegisterUserDTO(
    string NickName,
    string Login,
    string Password);
public record class LoginUserDTO(
    string Login,
    string Password);
public record class GetUserDTO(
    string NickName,
    string Login,
    List<GetInListChatDTO> Chats);