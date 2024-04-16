using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Response
{
    public class UserName
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
