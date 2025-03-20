using ExamenLibros.Extension;
using ExamenLibros.Filters;
using ExamenLibros.Models;
using ExamenLibros.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExamenLibros.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;

        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        [AuthorizeUsuarios]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Libros(int? idgenero)
        {
            List<Libro> libros;
            if(idgenero != null)
            {
                libros = await this.repo.GetLibrosByGeneroAsync(idgenero.Value);
            }
            else
            {
                libros = await this.repo.GetLibrosAsync();
            }
            return View(libros);
        }

        public async Task<IActionResult> Details(int idlibro)
        {
            Libro libro = await this.repo.FindLibroAsync(idlibro);
            return View(libro);
        }

        public IActionResult ComprarLibro(int? idlibro)
        {
            if (idlibro != null)
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("CARRITO") == null)
                    carrito = new List<int>();
                else
                    carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                carrito.Add(idlibro.Value);
                HttpContext.Session.SetObject("CARRITO", carrito);
            }
            return RedirectToAction("Carrito");
        }

        public async Task<IActionResult> QuitarLibro(int? idlibro)
        {
            if (idlibro != null)
            {
                List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                carrito.Remove(idlibro.Value);
                if (carrito.Count() == 0)
                    HttpContext.Session.Remove("CARRITO");
                else
                    HttpContext.Session.SetObject("CARRITO", carrito);
            }
            return RedirectToAction("Carrito");
        }

        public async Task<IActionResult> Carrito()
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            if (carrito != null)
            {
                List<Libro> libro = await this.repo.GetLibrosCarritoAsync(carrito);
                return View(libro);
            }
            return View();
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> FinalizarCompra()
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await this.repo.FinalizarPedidoAsync(carrito, idusuario);
            HttpContext.Session.Remove("CARRITO");
            return RedirectToAction("ComprasUsuario");
        }

        public async Task<IActionResult> ComprasUsuario()
        {
            int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<VistaPedido> vistaPedido = await this.repo.GetPedidosUsuarioAsync(idusuario);
            return View(vistaPedido);
        }

        public IActionResult _Perfil()
        {
            return PartialView("_Perfil");
        }
    }
}
