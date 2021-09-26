using System;
using System.Threading.Tasks;
using System.Collections.Generic;
//dtos
using DecolaTech.CatalogoJogos.Domain.Models.Views;
using DecolaTech.CatalogoJogos.Domain.Models.Inputs;

namespace DecolaTech.CatalogoJogos.Domain.Interfaces.Services
{
    public interface IJogoService : IDisposable
    {
         Task<List<JogoViewModel>> Obter (int pagina, int quantidade); // Retorn lista de jogos
         Task<JogoViewModel> Obter (Guid id); // Retorna jogo por id
         Task<JogoViewModel> Inserir(JogoInputModel jogo); // Insere um novo Jogo
         Task Atualizar(Guid id , JogoInputModel jogo);//Atualiza Jogo
         Task Atualizar(Guid id, double preco); //Atualiza Preco por id
         Task Remover(Guid id); // Remove Jogo Por id
    }
}