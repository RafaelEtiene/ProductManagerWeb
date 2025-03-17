using ProductManager.Models.Entities;
using ProductManager.Repositories;
using System.Linq.Expressions;

namespace ProductManager.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return await _repository.GetProdutosAsync();
        }

        public async Task<Produto?> GetProdutoByIdAsync(int id)
        {
            return await _repository.GetProdutoByIdAsync(id);
        }

        public async Task InsertProdutoAsync(Produto produto)
        {
            await _repository.InsertProdutoAsync(produto);
        }

        public async Task UpdateProdutoAsync(Produto produto)
        {
            await _repository.UpdateProdutoAsync(produto);
        }

        public async Task DeleteProdutoAsync(int id)
        {
            await _repository.DeleteProdutoAsync(id);
        }

        public async Task<bool> ValidarProdutosAsync(Expression<Func<Produto, bool>> predicate)
        {
            return await _repository.ValidarProdutosAsync(predicate);
        }
    }
}
