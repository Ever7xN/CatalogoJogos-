using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Exceptions
{
    public class JogoCadastradoException : Exception
    {
        public JogoCadastradoException()
            : base("Esse jogo já foi cadastrado")
        { }
    }
}
