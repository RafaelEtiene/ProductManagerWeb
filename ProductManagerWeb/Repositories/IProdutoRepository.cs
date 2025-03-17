﻿using ProductManager.Models.Entities;
using System.Linq.Expressions;

namespace ProductManager.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task<Produto?> GetProdutoByIdAsync(int id);
        Task InsertProdutoAsync(Produto produto);
        Task UpdateProdutoAsync(Produto produto);
        Task DeleteProdutoAsync(int id);
        Task<bool> ValidarProdutosAsync(Expression<Func<Produto, bool>> predicate);
    }
}
