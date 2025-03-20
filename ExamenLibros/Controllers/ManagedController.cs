using ExamenLibros.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ExamenLibros.Models;
using System.Security.Claims;

namespace ExamenLibros.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryLibros repo;

        public ManagedController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string pass)
        {
            Usuario usuario = await this.repo.LoginUsuarioAsync(email, pass);

            if (usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role
                );

                Claim claimEmail = new Claim(ClaimTypes.Name, usuario.Email);
                Claim claimApellido = new Claim("Apellidos", usuario.Apellidos);
                Claim claimFoto = new Claim("Foto", usuario.Foto);
                Claim claimNombre = new Claim("Nombre", usuario.Nombre);
                Claim claimID = new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());

                identity.AddClaim(claimEmail);
                identity.AddClaim(claimApellido);
                identity.AddClaim(claimFoto);
                identity.AddClaim(claimNombre);
                identity.AddClaim(claimID);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                string controller = TempData["controller"]?.ToString() ?? "Libros";
                string action = TempData["action"]?.ToString() ?? "Index";

                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["MENSAJE"] = "Email/password incorrectos";
                return View();
            }
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Managed");
        }
    }
}
