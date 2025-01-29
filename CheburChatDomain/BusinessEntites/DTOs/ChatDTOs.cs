namespace Domain.BusinessEntites.DTOs;

public record class CreateChatDTO(string Name);
public record class GetChatDTO(
    string Name,
    List<GetMessageDTO> Messages,
    List<string> NickNames);
public record class GetInListChatDTO(
    string Name,
    Guid Id);