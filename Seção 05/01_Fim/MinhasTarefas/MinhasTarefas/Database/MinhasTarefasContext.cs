using Microsoft.EntityFrameworkCore;
using MinhasTarefas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefas.Database
{
    public class MinhasTarefasContext:DbContext
    {
        public MinhasTarefasContext(DbContextOptions<MinhasTarefasContext> options):base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
