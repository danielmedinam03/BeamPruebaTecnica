using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Context.Seed
{
    public class RolSeedConfig
    {
        //Se realiza el cargue predeterminado de estos datos
        public RolSeedConfig(EntityTypeBuilder<Rol> entity)
        {
            entity.HasData(
                new Rol
                {
                    Id = 1,
                    Nombre = "Administrador",
                    Estado = true
                },
                new Rol
                {
                    Id = 2,
                    Nombre = "Estudiante",
                    Estado = true
                },
                new Rol
                {
                    Id = 3,
                    Nombre = "Profesor",
                    Estado = true
                });
        }
    }
}
