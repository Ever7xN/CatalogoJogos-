using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public interface IJogosServices : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int paginas, int quantidade);
        Task<JogoViewModel> Obter(Guid idJogo);
        Task<JogoViewModel> Inserir(JogoInputModel jogo);
        Task Atualizar(Guid idJogo, JogoInputModel jogo);
        Task Atualizar(Guid idJogo, double preco);
        Task Apagar(Guid idJogo);
    }
}
