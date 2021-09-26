using System;
namespace DecolaTech.CatalogoJogos.Domain.Exceptions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException() : base("Jogo Não Cadastrado.")
        { }
    }
}