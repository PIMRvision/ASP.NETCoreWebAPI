using MinhasTarefas.Database;
using MinhasTarefas.Models;
using MinhasTarefas.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasTarefas.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly MinhasTarefasContext _banco;
        public TarefaRepository(MinhasTarefasContext banco)
        {
            _banco = banco;
        }

        public List<Tarefa> Restauracao(ApplicationUser usuario, DateTime dataUltimaSincronizacao)
        {
            var query = _banco.Tarefas.Where(a => a.UsuarioID == usuario.Id).AsQueryable();

            if (dataUltimaSincronizacao == null)
            {
                query.Where(a => a.Criado >= dataUltimaSincronizacao || a.Atualizado >= dataUltimaSincronizacao);
            }

            return query.ToList<Tarefa>();
        }

        public List<Tarefa> Sincronizacao(List<Tarefa> tarefas)
        {
            var tarefasNovas = tarefas.Where(a => a.IdTarefaApi == 0);
            // Cadastrar novos registros
            if (tarefasNovas.Count() > 0)
            {
                foreach (var tarefa in tarefasNovas)
                {
                    _banco.Tarefas.Add(tarefa);
                }
            }

            var tarefasExcluidasAtualizadas = tarefas.Where(a => a.IdTarefaApi != 0);
            //Atualização de registro (Excluido)
            if (tarefasExcluidasAtualizadas.Count() > 0)
            {
                foreach (var tarefa in tarefasExcluidasAtualizadas)
                {
                    _banco.Tarefas.Update(tarefa);
                }                
            }

            _banco.SaveChanges();

            return tarefasNovas.ToList();
        }
    }
}
