using System;
using System.Web.Mvc;
using TokenINFRA.Entidades;
using TokenINFRA.Regras;
using TokenINFRA.Repositorio;

namespace TokenWEB.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly RegistrarUsuario _repository;

        public UsuarioController()
        {
            _repository = new RegistrarUsuario();
        }

        // GET: RegisterUser/Create
        public ActionResult Create()
        {
            return View(new Usuario());
        }

        // POST: RegisterUser/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Create", usuario);

                // Validating Username 
                if (_repository.ValidarNomeUsuario(usuario))
                {
                    ModelState.AddModelError("", @"O usuário já está registrado.");
                    return View("Create", usuario);
                }
                usuario.CriadoEm = DateTime.Now;

                usuario.Senha = Criptografia.RetornaSenhaCriptografada(usuario.Senha);

                // Saving User Details in Database
                _repository.Adicionar(usuario);

                TempData["UserMessage"] = "User Registered Successfully";

                ModelState.Clear();
                return View("Create", new Usuario());
            }
            catch
            {
                return View();
            }
        }

    }
}