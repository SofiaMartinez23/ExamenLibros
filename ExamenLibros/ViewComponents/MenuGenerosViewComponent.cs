using ExamenLibros.Models;
using ExamenLibros.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenLibros.ViewComponents
{
    public class MenuGenerosViewComponent : ViewComponent
    {

        private RepositoryLibros repo;

        public MenuGenerosViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> genero = await this.repo.GetGenerosAsync();
            return View(genero);
        }
    }
}
