using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.FluentConfig
{
    public class LoginEventConfig
    {
        public LoginEventConfig(EntityTypeBuilder<LoginEvent> entity)
        {
            entity.ToTable("LoginEvent");

            entity.HasKey(x => x.Id);

            entity.Property(p => p.UserId).IsRequired();
            entity.Property(p => p.HoraIngreso).IsRequired();
            entity.Property(p => p.Resultado).IsRequired();
        }
    }
}
