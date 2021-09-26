using System;
using System.Runtime.Serialization;

namespace DecolaTech.CatalogoJogos.Domain.Exceptions
{
    public class JogoJaCadastradoException : Exception 
    {
        public JogoJaCadastradoException() : base("Jogo já está Cadastrado!")
        {  }
    }
}