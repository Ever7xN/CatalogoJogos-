using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : IJogosRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Jogo{ IdJogo = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Counter Strike", Produtora = "Valve", Preco = 30 }},
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Jogo{ IdJogo = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Madden 22", Produtora = "EA", Preco = 230 } },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Jogo{ IdJogo = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Red Dead Redemption 2", Produtora = "Rockstar", Preco = 180 } },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Jogo{ IdJogo = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Final Fantasy XV", Produtora = "Square", Preco = 150} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Jogo{ IdJogo = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Cyberpunk 2077", Produtora = "Projekt", Preco = 60 } },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Jogo{ IdJogo = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "The Elder Scrolls V", Produtora = "Bethesda", Preco = 70 } }
        };

        public Task<List<Jogo>> Obter(int paginas, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((paginas - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid idJogo)
        {
            if (!jogos.ContainsKey(idJogo))
                return null;

            return Task.FromResult(jogos[idJogo]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.IdJogo, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.IdJogo] = jogo;
            return Task.CompletedTask;
        }

        public Task Apagar(Guid idJogo)
        {
            jogos.Remove(idJogo);
            return Task.CompletedTask;
        }

        public void Dispose()
        { }
    }
}
