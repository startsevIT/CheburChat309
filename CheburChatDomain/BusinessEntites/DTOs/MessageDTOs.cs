namespace Domain.BusinessEntites.DTOs;

public record class CreateMessageDTO(string Text);
public record class GetMessageDTO(
    string NickName,
    string Text,
    DateTime DateTime);
