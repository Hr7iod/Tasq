using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class TasqConfiguration : IEntityTypeConfiguration<Tasq>
    {
        public void Configure(EntityTypeBuilder<Tasq> builder)
        {
            builder.HasData
            (
                new Tasq
                {
                    Id = new Guid("BE8EECAD-96A0-4EBA-9BA2-14DD7A88F5D9"),
                    Name = "Первая тестовая таска",
                    Description = "Проверка дескрипшена"
                },
                new Tasq
                {
                    Id = new Guid("3B6B5DB1-45CA-460C-9BCA-725D2D3A6747"),
                    Name = "Вторая тестовая таска"
                },
                new Tasq
                {
                    Id = new Guid("D917BD64-22EE-4942-BD7C-DC5EF2132D23"),
                    Name = "Вторая тестовая ПОДтаска",
                    ParentId = new Guid("3B6B5DB1-45CA-460C-9BCA-725D2D3A6747")
                },
                new Tasq
                {
                    Id = new Guid("1C21F4B6-B7E5-45D8-A3DA-5FB19DCAB145"),
                    Name = "Вторая тестовая ПОДПОДтаска",
                    Description = "Проверка подПОДдескрипшена",
                    ParentId = new Guid("D917BD64-22EE-4942-BD7C-DC5EF2132D23")
                },
                new Tasq
                {
                    Id = new Guid("E7DF029A-FF87-4FF8-89AB-D22D8AC3CE29"),
                    Name = "Третья тестовая таска",
                    Description = "Проверка третий раз дескрипшена"
                }
            ) ;
        }
    }
}
