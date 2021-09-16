using ApiCatalogoJogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogosSqlServerRepository : IJogosRepository
    {
        private readonly SqlConnection sqlConnection;

        public JogosSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Jogo>> Obter(int paginas, int quantidade)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from jogos order by id offset {((paginas - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    IdJogo = (Guid)sqlDataReader["idJogo"],
                    Nome = (string)sqlDataReader["nome"],
                    Produtora = (string)sqlDataReader["produtora"],
                    Preco = (double)sqlDataReader["preco"]
                });                    
            }

            await sqlConnection.CloseAsync();

            return jogos;
        }

        public async Task<Jogo> Obter(Guid idJogo)
        {
            Jogo jogo = null;

            var comando = $"select * from where id = '{idJogo}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while(sqlDataReader.Read())
            {
                jogo = new Jogo
                {
                    IdJogo = (Guid)sqlDataReader["idJogo"],
                    Nome = (string)sqlDataReader["nome"],
                    Produtora = (string)sqlDataReader["produtora"],
                    Preco = (double)sqlDataReader["preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return jogo;
        }



    }
}
