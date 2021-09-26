
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using DecolaTech.CatalogoJogos.Domain.Interfaces.Services;
using DecolaTech.CatalogoJogos.Domain.Entities;
using DecolaTech.CatalogoJogos.Domain.Interfaces.Repositories;
using DecolaTech.CatalogoJogos.Domain.Models.Inputs;
using DecolaTech.CatalogoJogos.Domain.Models.Views;
using DecolaTech.CatalogoJogos.Domain.Exceptions;

namespace DecolaTech.CatalogoJogos.Services
{
    public class JogoService : IJogoService
    {

        private IJogoRepository _jogosRepository;

        public JogoService(IJogoRepository repository){
            this._jogosRepository = repository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
      
            //Repository so conhece a entidade
            var jogos = await _jogosRepository.Obter(pagina, quantidade);
            //Serialização para o model
            var lista = jogos.Select(jogo => new JogoViewModel{
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            });

            return lista.ToList();
            

        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogosRepository.Obter(id);

            if(jogo == null)
                return null;

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var jogoExists = await _jogosRepository.Obter(jogo.Nome, jogo.Produtora);
            if(jogoExists.Count() > 0)
                throw new JogoJaCadastradoException();

            var jogoToInsert = new Jogo 
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogosRepository.Inserir(jogoToInsert);

            return new JogoViewModel 
            {
                Id= jogoToInsert.Id,
                Nome=  jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Atualizar(Guid id, JogoInputModel input)
        {

            var jogo = await _jogosRepository.Obter(id);
            if(jogo == null)
                throw new JogoNaoCadastradoException();

            jogo.Nome = input.Nome;
            jogo.Preco =  input.Preco;
            jogo.Produtora = input.Produtora;

            await _jogosRepository.Atualizar(jogo);

        }

        public async Task Atualizar(Guid id, double preco)
        {
            var jogoExists = await _jogosRepository.Obter(id);
            if(jogoExists == null)
                throw new JogoNaoCadastradoException();

            jogoExists.Preco = preco;

            await _jogosRepository.Atualizar(jogoExists);
            
        }

        public async Task Remover(Guid id)
        {
            var jogoExists = await _jogosRepository.Obter(id);
            if(jogoExists == null)
                throw new JogoNaoCadastradoException();

            await _jogosRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogosRepository?.Dispose();
        }
    }
}