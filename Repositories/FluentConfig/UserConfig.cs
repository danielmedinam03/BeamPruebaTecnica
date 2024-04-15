using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.FluentConfig
{
    public class UserConfig
    {
        public UserConfig(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");

            entity.HasKey(p => p.UserId);

            entity.HasOne(p => p.Rol)
                .WithOne(p => p.User)
                .HasForeignKey<User>(p => p.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.Nombre).IsRequired();
            entity.Property(p => p.Apellidos).IsRequired();
            entity.Property(p => p.NombreUsuario).IsRequired();
            entity.Property(p => p.Contraseña).IsRequired();
            entity.Property(p => p.FechaNacimiento).IsRequired();
            entity.Property(p => p.Celular);
            entity.Property(p => p.Estado).IsRequired();
            entity.Property(p => p.Eliminado).IsRequired();
            entity.Property(p => p.FechaEliminacion);
        }
    }
}
