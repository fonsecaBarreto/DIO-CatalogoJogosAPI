using System;

namespace DecolaTech.CatalogoJogos.Domain.Models.Views
{
    public class JogoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Produtora { get; set; }
        public double Preco { get; set; }

    }
}