using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public class JogoServices : IJogosServices
    {
        private readonly IJogosRepository _jogosRepository;

        public JogoServices(IJogosRepository jogosRepository)
        {
            _jogosRepository = jogosRepository;
        }
        
        public async Task<List<JogoViewModel>> Obter(int paginas, int quantidade)
        {
            var jogos = await _jogosRepository.Obter(paginas, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.IdJogo,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            })
                .ToList();
        }

        public async Task<JogoViewModel> Obter(Guid idJogo)
        {
            var jogo = await _jogosRepository.Obter(idJogo);

            if (jogo == null)
                return null;

            return new JogoViewModel
            {
                Id = jogo.IdJogo,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var objJogo = await _jogosRepository.Obter(jogo.Nome, jogo.Produtora);

            if (objJogo.Count() > 0)
                throw new JogoCadastradoException();

            var jogoInserir = new Jogo
            {
                IdJogo = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogosRepository.Inserir(jogoInserir);

            return new JogoViewModel
            {
                Id = jogoInserir.IdJogo,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Atualizar(Guid idJogo, JogoInputModel jogo)
        {
            var objJogo = await _jogosRepository.Obter(idJogo);

            if (objJogo == null)
                throw new JogoCadastradoException();

            objJogo.Nome = jogo.Nome;
            objJogo.Produtora = jogo.Produtora;
            objJogo.Preco = jogo.Preco;

            await _jogosRepository.Atualizar(objJogo);
        }

        public async Task Atualizar(Guid idJogo, double preco)
        {
            var objJogo = await _jogosRepository.Obter(idJogo);

            if (objJogo == null)
                throw new JogoCadastradoException();

            objJogo.Preco = preco;

            await _jogosRepository.Atualizar(objJogo);
        }

        public async Task Apagar(Guid idJogo)
        {
            var jogo = await _jogosRepository.Obter(idJogo);

            if (jogo == null)
                throw new JogoCadastradoException();

            await _jogosRepository.Apagar(idJogo);
        }

        public void Dispose()
        {
            _jogosRepository?.Dispose();
        }
    }
}
