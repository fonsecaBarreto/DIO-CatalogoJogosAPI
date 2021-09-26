using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DecolaTech.CatalogoJogos.Domain.Entities;
using DecolaTech.CatalogoJogos.Domain.Interfaces.Repositories;
using System.Linq;



namespace DecolaTech.CatalogoJogos.Data
{

    public class JogoRepository : IJogoRepository
    {

        public static Jogo MakeJogo(int n){
            Guid[] ids = new Guid[4]{
                Guid.Parse("83f9ba73-9d8c-475c-8c33-d71ebdf385ca"),
                Guid.Parse("346f8d01-4638-4c1b-9520-0fa484f6e9b9"),
                Guid.Parse("0563b179-d889-47c3-b649-77a705da0fb2"),
                Guid.Parse("c3497b48-faec-4b43-a915-de9ba27002a5")
            };

            return new Jogo{
                Id= ids[n],
                Nome= $"Nome Teste {n}",
                Produtora = $"Produtora Teste {n}",
                Preco = 5.32
            };
  
        }
        private static Dictionary<Guid, Jogo> _jogos = new Dictionary<Guid, Jogo>(){
            { MakeJogo(0).Id, MakeJogo(0) },
            { MakeJogo(1).Id, MakeJogo(1) },
            { MakeJogo(2).Id, MakeJogo(2) },
            { MakeJogo(3).Id, MakeJogo(3) },
        };


        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(_jogos.Values.Skip((pagina) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {

            if(!_jogos.ContainsKey(id)){
                return Task.FromResult<Jogo>(null);
            }

            return Task.FromResult(_jogos[id]);
        }


        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
           return Task.FromResult(_jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task Inserir(Jogo jogo)
        {
            _jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

     
    
        public Task Atualizar(Jogo jogo)
        {
            _jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            _jogos.Remove(id);
            return Task.CompletedTask;
        }


        public void Dispose()
        {
            //Fechar Conexão com banco de dados
        }

    }
}