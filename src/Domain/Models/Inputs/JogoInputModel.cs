using System;
using System.ComponentModel.DataAnnotations;

namespace DecolaTech.CatalogoJogos.Domain.Models.Inputs
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome do Jogo deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O Nome da Produtora deve conter valor valido")]
        public string Produtora { get; set; }

        [Required]
        [Range(1,1000, ErrorMessage = "O Preco deve ser no minio R$: 1,0  e no maximo R$:1000,0 Reais")]
        public double Preco { get; set; }

    }
}