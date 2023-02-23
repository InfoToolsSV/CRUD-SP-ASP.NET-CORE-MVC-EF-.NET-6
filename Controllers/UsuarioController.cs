using Microsoft.AspNetCore.Mvc;
using MVC_EF.Data;
using MVC_EF.Models;

namespace MVC_EF.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DbCon _context;

        public UsuarioController(DbCon con)
        {
            _context = con;
        }
        public IActionResult Index()
        {
            var usuarios = _context.ObtenerUsuariosSP().ToList();
            return View(usuarios);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Usuario usuario)
        {
            if (ModelState.IsValid && usuario.Nombre is not null)
            {
                _context.CrearUsuarioSP(usuario.Nombre, usuario.Edad);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Actualizar(int id)
        {
            var usuario = _context.ObtenerUsuarioPorIdSP(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Actualizar(Usuario usuario)
        {
            if (ModelState.IsValid && usuario.Nombre is not null && usuario.Id_Usuario > 0)
            {
                _context.ActualizarUsuarioSP(usuario.Id_Usuario, usuario.Nombre, usuario.Edad);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Eliminar(int id)
        {
            var usuario = _context.ObtenerUsuarioPorIdSP(id);
            return View(usuario);
        }


        [HttpPost]
        public IActionResult Eliminar(Usuario usuario)
        {
            if (usuario.Id_Usuario > 0)
            {
                _context.EliminarUsuarioSP(usuario.Id_Usuario);
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}