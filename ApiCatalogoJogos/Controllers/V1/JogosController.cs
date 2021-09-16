using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogosServices _jogosServices;
        public JogosController(IJogosServices jogosServices)
        {
            _jogosServices = jogosServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int paginas = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogosServices.Obter(paginas, quantidade);

            if (jogos.Count() == 0)          
                return NoContent();
            
            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await _jogosServices.Obter(idJogo);

            if (jogo == null)
                return NoContent();

            return Ok();
        }   

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInput)
        {
            try
            {
                var jogo = await _jogosServices.Inserir(jogoInput);

                return Ok();
            }
            catch(JogoCadastradoException e)
            {
                return UnprocessableEntity("Jogo já cadastrado");
            }              
        }

        [HttpPut ("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInput)
        {
            try
            {
                await _jogosServices.Atualizar(idJogo, jogoInput);

                return Ok();
            }
            catch(JogoNCadastradoException e)
            {
                return NotFound("Jogo não cadastrado");
            }            
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] double preco)
        {
            try
            {
                await _jogosServices.Atualizar(idJogo, preco);

                return Ok();
            }
            catch(JogoNCadastradoException e)
            {
                return NotFound("Jogo não cadastrado");
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogosServices.Apagar(idJogo);

                return Ok();
            }
            catch(JogoNCadastradoException e)
            {
                return NotFound("Jogo não cadastrado");
            }
        }
    }
    }

