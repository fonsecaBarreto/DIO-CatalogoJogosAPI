using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//Domain
using DecolaTech.CatalogoJogos.Domain.Entities;
using DecolaTech.CatalogoJogos.Domain.Exceptions;
using DecolaTech.CatalogoJogos.Domain.Models.Views;
using DecolaTech.CatalogoJogos.Domain.Models.Inputs;
using DecolaTech.CatalogoJogos.Domain.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace DecolaTech.CatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
     
        private readonly IJogoService _jogosServices;

        public JogosController (IJogoService jogoService) {
            _jogosServices = jogoService;
        }

     
        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 0</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 0 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response> 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(0, int.MaxValue)] int pagina = 0, [FromQuery, Range(1, 50)] int quantidade = 3){
            var jogos = await _jogosServices.Obter(pagina, quantidade);
            if(jogos.Count() == 0 ) return NoContent();
            return Ok(jogos);
      
        }

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>   

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo){
            var jogoView = await _jogosServices.Obter(idJogo);
            if(jogoView == null){
                return NoContent();
            }
            return Ok(jogoView);
        }  

        /// <summary>
        /// Inserir um jogo no catálogo
        /// </summary>
        /// <param name="jogoInput">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response>   

        [HttpPost("")]
        public async Task<ActionResult<JogoViewModel>> InserirJogo ([FromBody] JogoInputModel jogoInput ) {

            try{
                var jogo = await _jogosServices.Inserir(jogoInput);
                return Ok(jogo);
            }catch(JogoJaCadastradoException e ){
                Console.WriteLine(e);
                return UnprocessableEntity("Sinto muito. Já Existe um jogo com este nome para essa produtora.");
            }

        }

        /// <summary>
        /// Atualizar um jogo no catálogo
        /// </summary>
        /// <param name="id">Id do jogo a ser atualizado</param>
        /// <param name="jogoInput">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Cao o jogo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> AtualizarJogo ([FromRoute] Guid id, [FromBody] JogoInputModel jogoInput ) {

            try{
                await _jogosServices.Atualizar(id, jogoInput);
                return Ok();

            }catch(JogoNaoCadastradoException err ){
                Console.WriteLine(err);
                return NotFound("Jogo Não Existe");
            }

        }


        /// <summary>
        /// Atualizar o preço de um jogo
        /// </summary>
        /// <param name="id">Id do jogo a ser atualizado</param>
        /// <param name="preco">Novo preço do jogo</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   

        [HttpPatch("{id:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute]Guid id,[FromRoute] double preco){
            try{
                await _jogosServices.Atualizar(id, preco);
                return Ok();
            }catch(JogoNaoCadastradoException err ){
                Console.WriteLine(err);
                return NotFound("Jogo Não Existe");
            }
        }

        /// <summary>
        /// Excluir um jogo
        /// </summary>
        /// <param name="id">Id do jogo a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>  

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeletarJogo([FromRoute]Guid id){
            try{
                await _jogosServices.Remover(id);
                return Ok();

            }catch(JogoNaoCadastradoException err ){
                Console.WriteLine(err);
                return NotFound("Jogo Não Existe");
            }
        }
    }
}