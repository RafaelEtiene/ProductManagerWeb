using Microsoft.AspNetCore.Mvc;
using ProductManager.Models.Entities;
using ProductManager.Services;

namespace ProductManager.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetProdutosAsync());
        }

        public IActionResult Create()
        {
            return View(new Produto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                var produtoExistente = await _service.ValidarProdutosAsync(e => e.Nome.ToLower() == produto.Nome.ToLower());
                if (produtoExistente)
                {
                    ModelState.AddModelError("Nome", "Já existe um produto com esse nome!");
                    return View(produto);
                }
                await _service.InsertProdutoAsync(produto);
                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }


        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var produto = await _service.GetProdutoByIdAsync(Id.Value);

            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Descricao")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var produtoExistente = await _service.ValidarProdutosAsync(e => e.Nome.ToLower() == produto.Nome.ToLower() && e.Id != produto.Id);
                if (produtoExistente)
                {
                    ModelState.AddModelError("Nome", "Já existe um produto com esse nome!");
                }

                await _service.UpdateProdutoAsync(produto);
                return RedirectToAction(nameof(Index));
            }

            return View(produto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _service.GetProdutoByIdAsync(id.Value);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteProdutoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
