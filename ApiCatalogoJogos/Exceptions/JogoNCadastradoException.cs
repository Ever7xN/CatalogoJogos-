using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Exceptions
{
    public class JogoNCadastradoException : Exception
    {
        public JogoNCadastradoException()
            : base("Jogo não cadastrado")
        { }                       
    }
}
