using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Servicos;
using MongoDB.Bson;
using web_renderizacao_server_side.Helpers;

namespace api.Controllers
{
    [ApiController]
    [Logado]
    public class PaisController : ControllerBase
    {
        public PaisController()
        {
            this.paiMongoRepo = new PaisMongodb();
        }

        private PaisMongodb paiMongoRepo;       

        // GET: /pais
        [HttpGet]
        [Route("/pais/")]
        public async Task<IActionResult> Index()
        {
            //   var paisPaginados = await _context.Pais.OrderBy(m => m.Id)
            //         .Skip((page - 1) * qtdPage)
            //         .Take(qtdPage)
            //         .ToListAsync();

            //   return StatusCode(200, paisPaginados);

             return StatusCode(200, await paiMongoRepo.Todos());
        }

        // GET: /pais/5
        [HttpGet]
        [Route("/pais/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pai = await paiMongoRepo.BuscaPorId(ObjectId.Parse(id));
            if (pai == null)
            {
                return NotFound();
            }

            return StatusCode(200, pai);
        }

        // POST: /pais
        [HttpPost]
        [Route("/pais")]
        public async Task<IActionResult> Create(Pai pai)
        {
            if (ModelState.IsValid)
            {
                if(! await AlunoServico.ValidarAluno(pai.AlunoId))
                {
                    return StatusCode(400, new {Mensagem = "O aluno passado não é válido ou não está cadastrado"});
                }

                paiMongoRepo.Inserir(pai);
                return  StatusCode(201, pai);
            }
            return StatusCode(400, new {Mensagem = "O pai passado é inválido"});
        }

        // PUT: /pais/5
        [HttpPut]
        [Route("/pais/{id}")]
        public async Task<IActionResult> Edit(string id,Pai pai)
        {
            if (ModelState.IsValid)
            {
                if(! await AlunoServico.ValidarAluno(pai.AlunoId))
                {
                    return StatusCode(400, new {Mensagem = "O aluno passado não é válido ou não está cadastrado"});
                }
                
                try
                {
                    pai.Id = id;
                    paiMongoRepo.Atualizar(pai);
                }
                catch (Exception erro)
                {
                    if (! await PaiExists(pai.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return StatusCode(500, erro);
                    }
                }
                return StatusCode(200, pai);
            }
            return StatusCode(200, pai);
        }

        // DELETE: /pais/5
        [HttpDelete]
        [Route("/pais/{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            if (paiMongoRepo.BuscaPorId(ObjectId.Parse(id)) == null)
            {
                return Problem("Entity set 'DbContexto.Pais'  is null.");
            }
            paiMongoRepo.RemovePorId(ObjectId.Parse(id));
            return StatusCode(204);
        }

        private async Task<bool> PaiExists(ObjectId id)
        {
          return (await paiMongoRepo.BuscaPorId(id)) != null;
        }
    }
}
