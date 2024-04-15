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
    public class RolConfig
    {
        public RolConfig(EntityTypeBuilder<Rol> entity)
        {
            entity.ToTable("Rol");

            entity.HasKey(x => x.Id);

            entity.Property(p => p.Nombre).IsRequired();
            entity.Property(p => p.Estado).IsRequired();
        }
    }
}
