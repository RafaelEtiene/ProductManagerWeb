﻿using System.ComponentModel.DataAnnotations;

namespace ProductManager.Models.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
