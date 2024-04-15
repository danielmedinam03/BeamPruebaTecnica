using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class LoginEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int UserId { get; set; }
        public DateTime HoraIngreso { get; set; }
        /// <summary>
        /// Si el login fue exitoso o no
        /// </summary>
        public bool Resultado { get; set; }
    }
}
