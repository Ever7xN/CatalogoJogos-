using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public interface IJogosRepository : IDisposable
    {
        Task<List<Jogo>> Obter(int quantidade, int paginas);
        Task<Jogo> Obter(Guid idJogo);
        Task<List<Jogo>> Obter(string nome, string produtora);
        Task Inserir(Jogo jogo);
        Task Atualizar(Jogo jogo);
        Task Apagar(Guid idJogo);
    }
}
