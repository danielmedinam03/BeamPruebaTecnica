using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
    public class UserResponseDto
    {
        [Key]
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public long Celular { get; set; }
        /// <summary>
        /// Si el usuario esta activo o no
        /// </summary>
        public bool Estado { get; set; }
        public string Rol { get; set; }
    }
}
