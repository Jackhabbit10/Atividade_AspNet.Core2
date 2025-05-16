using AtividadeCidade.Repositorio;
using Microsoft.AspNetCore.Mvc;


namespace AtividadeCidade.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _usuarioRepositorio.ObterUsuario(email);

            if (usuario != null && usuario.senha_usu == senha)
            {
                // Autenticação bem-sucedida
                // Redireciona o usuário para a action "Index" do Controller "Cliente".
                return RedirectToAction("Index", "Produto");
            }

            ModelState.AddModelError("", "Email ou senha inválidos.");

            //retorna view Login 
            return View();
        }
    }
}
