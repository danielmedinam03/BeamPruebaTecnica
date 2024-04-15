using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public virtual User User { get; set; }
    }
}
