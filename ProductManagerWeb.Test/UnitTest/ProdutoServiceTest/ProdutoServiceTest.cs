using Xunit;
using Moq;
using ProductManager.Services;
using ProductManager.Repositories;
using ProductManager.Models.Entities;
using System.Linq.Expressions;

namespace ProductManagerWeb.Test.UnitTest.ProdutoServiceTest
{
    public class ProdutoServiceTests
    {
        private readonly ProdutoService _service;
        private readonly Mock<IProdutoRepository> _repositoryMock;

        public ProdutoServiceTests()
        {
            _repositoryMock = new Mock<IProdutoRepository>();
            _service = new ProdutoService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetProdutosAsync_ShouldReturnListOfProdutos()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { Nome = "Produto 1" },
                new Produto { Nome = "Produto 2" }
            };
            _repositoryMock.Setup(r => r.GetProdutosAsync()).ReturnsAsync(produtos);

            // Act
            var result = await _service.GetProdutosAsync();

            // Assert
            Assert.Equal(2, result.Count());
            _repositoryMock.Verify(r => r.GetProdutosAsync(), Times.Once);
        }

        [Fact]
        public async Task GetProdutoByIdAsync_ShouldReturnProduto_WhenProdutoExists()
        {
            // Arrange
            var produto = new Produto { Id = 1, Nome = "Produto Teste" };
            _repositoryMock.Setup(r => r.GetProdutoByIdAsync(1)).ReturnsAsync(produto);

            // Act
            var result = await _service.GetProdutoByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Produto Teste", result.Nome);
            _repositoryMock.Verify(r => r.GetProdutoByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetProdutoByIdAsync_ShouldReturnNull_WhenProdutoDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetProdutoByIdAsync(1)).ReturnsAsync((Produto?)null);

            // Act
            var result = await _service.GetProdutoByIdAsync(1);

            // Assert
            Assert.Null(result);
            _repositoryMock.Verify(r => r.GetProdutoByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task InsertProdutoAsync_ShouldCallRepositoryInsert()
        {
            // Arrange
            var produto = new Produto { Nome = "Novo Produto" };

            // Act
            await _service.InsertProdutoAsync(produto);

            // Assert
            _repositoryMock.Verify(r => r.InsertProdutoAsync(produto), Times.Once);
        }

        [Fact]
        public async Task UpdateProdutoAsync_ShouldCallRepositoryUpdate()
        {
            // Arrange
            var produto = new Produto { Id = 1, Nome = "Produto Atualizado" };

            // Act
            await _service.UpdateProdutoAsync(produto);

            // Assert
            _repositoryMock.Verify(r => r.UpdateProdutoAsync(produto), Times.Once);
        }

        [Fact]
        public async Task DeleteProdutoAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var id = 1;

            // Act
            await _service.DeleteProdutoAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.DeleteProdutoAsync(id), Times.Once);
        }

        [Fact]
        public async Task ValidarProdutosAsync_ShouldReturnTrue_WhenPredicateMatches()
        {
            // Arrange
            Expression<Func<Produto, bool>> predicate = p => p.Nome == "Produto Especial";
            _repositoryMock.Setup(r => r.ValidarProdutosAsync(predicate)).ReturnsAsync(true);

            // Act
            var result = await _service.ValidarProdutosAsync(predicate);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(r => r.ValidarProdutosAsync(predicate), Times.Once);
        }

        [Fact]
        public async Task ValidarProdutosAsync_ShouldReturnFalse_WhenPredicateDoesNotMatch()
        {
            // Arrange
            Expression<Func<Produto, bool>> predicate = p => p.Nome == "Produto Inexistente";
            _repositoryMock.Setup(r => r.ValidarProdutosAsync(predicate)).ReturnsAsync(false);

            // Act
            var result = await _service.ValidarProdutosAsync(predicate);

            // Assert
            Assert.False(result);
            _repositoryMock.Verify(r => r.ValidarProdutosAsync(predicate), Times.Once);
        }
    }
}
