using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimicAPI.Database;
using MimicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MimicAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        private readonly MimicContext _banco;

        public PalavrasController(MimicContext banco)
        {
            _banco = banco;
        }

        //APP -- /api/palavras
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodas(DateTime? data, int? pagNumero, int? pagRegistroPag)
        {
            var item = _banco.Palavras.AsQueryable();

            if (data.HasValue)
            {
                item = item.Where(a => a.Criado > data.Value || a.Atualizado > data.Value);
            }
            //return new JsonResult(_banco.Palavras);
            return Ok(item);

            if (pagNumero.HasValue)
            {
                item = item.Skip((pagNumero.Value - 1) * pagRegistroPag.Value).Take(pagRegistroPag.Value);
            }
        }

        //WEB -- /api/palavras/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var obj = _banco.Palavras.Find(id);
            if (obj == null)
                return NotFound();

            return Ok(_banco.Palavras.Find(id));
        }

        // -- /api/palavras (POST: id, nome, ativo, pontuacao, criacao)
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody]Palavra palavra)
        {

            palavra.Criado = DateTime.Now;
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();

            return Created($"/api/palavras/{palavra.Id}", palavra);
        }

        // -- /api/palavras/1 (PUT: id, nome, ativo, pontuacao, criacao)
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody]Palavra palavra)
        {
            var obj = _banco.Palavras.AsNoTracking().FirstOrDefault(a=>a.Id == id);
            if (obj == null)
                return NotFound();

            palavra.Id = id;
            palavra.Atualizado = DateTime.Now;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();

            return Ok(_banco.Palavras.Find(id));
        }

        // -- /api/palavras/1 (DELETE)
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var palavra = _banco.Palavras.Find(id);
            if (palavra == null)
                return NotFound();
            
            palavra.Ativo = false;
            palavra.Atualizado = DateTime.Now;
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();

            return NoContent();
        }
    }
}
