using AtividadeCidade.Models;
using AtividadeCidade.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace AtividadeCidade.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Index()
        {
            return View(_produtoRepositorio.TodosProdutos());
        }

        public IActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            _produtoRepositorio.Cadastrar(produto);
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult EditarProduto(int id)
        //{
        //    var produto = _produtoRepositorio.ObterProduto(id);
        //    if (produto == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(produto);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditarProduto(int id, [Bind("cod_prod, nome_prod, descricao_prod, preco_prod")] Produto produto)
        //{
        //    if (id != produto.cod_prod)
        //    {
        //        return BadRequest();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (_produtoRepositorio.Atualizar(produto))
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            ModelState.AddModelError("", "Ocorreu um erro ao editar.");
        //            return View(produto);
        //        }
        //    }
        //    return View(produto);
        //}

        public IActionResult ExcluirProduto(int id)
        {
            _produtoRepositorio.Excluir(id);

            return RedirectToAction(nameof(Index));
        }
    }
}