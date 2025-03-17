using Xunit;
using Microsoft.EntityFrameworkCore;
using ProductManager.Data;
using ProductManager.Models.Entities;
using ProductManager.Repositories;
using System.Linq.Expressions;
using System;

namespace ProductManagerWeb.Test.UnitTest.ProdutoRepositoryTest
{
    public class ProdutoRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly ProdutoRepository _repository;

        public ProdutoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Banco isolado por teste
            .Options;

            _context = new AppDbContext(options);
            _repository = new ProdutoRepository(_context);
        }

        [Fact]
        public async Task GetProdutosAsync_ShouldReturnAllProdutos()
        {
            // Arrange
            _context.Produtos.AddRange(
                new Produto { Nome = "Produto 1" },
                new Produto { Nome = "Produto 2" }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProdutosAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetProdutoByIdAsync_ShouldReturnCorrectProduto()
        {
            // Arrange
            var produto = new Produto { Nome = "Produto Teste" };
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetProdutoByIdAsync(produto.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produto.Nome, result.Nome);
        }

        [Fact]
        public async Task InsertProdutoAsync_ShouldAddProdutoToDatabase()
        {
            // Arrange
            var produto = new Produto { Nome = "Novo Produto" };

            // Act
            await _repository.InsertProdutoAsync(produto);
            var result = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == produto.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Novo Produto", result.Nome);
        }

        [Fact]
        public async Task UpdateProdutoAsync_ShouldUpdateProdutoInDatabase()
        {
            // Arrange
            var produto = new Produto { Nome = "Produto Antigo" };
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            // Act
            produto.Nome = "Produto Atualizado";
            await _repository.UpdateProdutoAsync(produto);
            var result = await _context.Produtos.FindAsync(produto.Id);

            // Assert
            Assert.Equal("Produto Atualizado", result.Nome);
        }

        [Fact]
        public async Task DeleteProdutoAsync_ShouldRemoveProdutoFromDatabase()
        {
            // Arrange
            var produto = new Produto { Nome = "Produto para Remover" };
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteProdutoAsync(produto.Id);
            var result = await _context.Produtos.FindAsync(produto.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ValidarProdutosAsync_ShouldReturnTrue_WhenProdutoMatchesCondition()
        {
            // Arrange
            var produto = new Produto { Nome = "Produto Especial" };
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            // Act
            bool exists = await _repository.ValidarProdutosAsync(p => p.Nome == "Produto Especial");

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ValidarProdutosAsync_ShouldReturnFalse_WhenProdutoDoesNotMatchCondition()
        {
            // Arrange
            _context.Produtos.Add(new Produto { Nome = "Outro Produto" });
            await _context.SaveChangesAsync();

            // Act
            bool exists = await _repository.ValidarProdutosAsync(p => p.Nome == "Produto Inexistente");

            // Assert
            Assert.False(exists);
        }
    }
}
