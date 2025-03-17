using Microsoft.EntityFrameworkCore;
using ProductManager.Data;
using ProductManager.Models.Entities;
using System.Linq.Expressions;

namespace ProductManager.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto?> GetProdutoByIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task InsertProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProdutoAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidarProdutosAsync(Expression<Func<Produto, bool>> predicate)
        {
            return await _context.Produtos.AnyAsync(predicate);
        }
    }
}
