namespace Domain.BusinessEntites.DTOs;

public record class CreateMessageDTO(
    string Text);
public record class ReadMessageDTO(
    string NickName,
    string Text,
    DateTime DateTime);
