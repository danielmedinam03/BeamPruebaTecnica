using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string  Apellidos { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public long Celular { get; set; }
        /// <summary>
        /// Si el usuario esta activo o no
        /// </summary>
        public bool Estado { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public bool Eliminado { get; set; }
        public DateTime? FechaEliminacion { get; set; } = null;
        //public virtual ICollection<LoginEvent> LoginEvent { get; set; }
    }
}
