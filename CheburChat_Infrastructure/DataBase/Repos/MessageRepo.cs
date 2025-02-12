﻿using Domain.BusinessEntites.DTOs;
using Domain.BusinessEntites.Entities;
using Domain.BusinessLogic;
using Domain.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.Repos;

public class MessageRepo : IMessageRepo
{
    public async void CreateAsync(CreateMessageDTO dto, Guid userId)
    {
        using SqLiteDbContext db = new();
        User? user = await db.Users.FindAsync(userId) 
            ?? throw new Exception("Not Found");
        Message message = dto.Map(user);
        db.Messages.Add(message);
        db.SaveChanges();
    }

    public async Task<GetMessageDTO> ReadAsync(Guid MessageId)
    {
        using SqLiteDbContext db = new();
        Message? message = await db.Messages
            .Include(x => x.User)
            .FirstAsync(x => x.Id == MessageId) 
            ?? throw new Exception("Not Found message");

        return message.Map(message.User);
    }
}
