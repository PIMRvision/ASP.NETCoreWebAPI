using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefas.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int FullName { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
