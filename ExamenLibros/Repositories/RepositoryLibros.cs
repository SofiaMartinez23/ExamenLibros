using ExamenLibros.Data;
using ExamenLibros.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenLibros.Repositories
{
    public class RepositoryLibros
    {

        private LibrosContext context;

        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }

        public async Task<List<Libro>> GetLibrosAsync()
        {
            return await this.context.Libros.ToListAsync();
        }

        public async Task<List<Genero>> GetGenerosAsync()
        {
            return await this.context.Generos.ToListAsync();
        }

        public async Task<List<Libro>> GetLibrosByGeneroAsync(int idgenero)
        {
            return await this.context.Libros
                .Where(x => x.IdGenero == idgenero).ToListAsync();
        }

        public async Task<Libro> FindLibroAsync(int idlibro)
        {
            return await this.context.Libros
                .FirstOrDefaultAsync(c => c.IdLibro == idlibro);
        }

        public async Task<Usuario> LoginUsuarioAsync(string email, string pass)
        {
            return await this.context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Pass == pass);
        }

        public async Task<List<Libro>> GetLibrosCompraAsync(List<int> compra)
        {
            return await this.context.Libros
                .Where(c => compra.Contains(c.IdLibro)).ToListAsync();
        }

        public async Task<int> GetMaxIdCompraAsync()
        {
            if (this.context.Pedidos.Count() == 0) return 1;
            else return await this.context.Pedidos
                    .MaxAsync(c => c.IdPedido) + 1;
        }

        public async Task<List<VistaPedido>> GetVistaPedidoAsync(int idusuario)
        {
            return await this.context.VistaPedidos
              .Where(vc => vc.IdUsuario == idusuario)
              .ToListAsync();
        }

        public async Task<List<Libro>> GetLibrosCarritoAsync(List<int> carrito)
        {
            return await this.context.Libros
                .Where(c => carrito.Contains(c.IdLibro))
                .ToListAsync();
        }

        public async Task<List<VistaPedido>> GetPedidosUsuarioAsync(int idusuario)
        {
            return await this.context.VistaPedidos
                .Where(vc => vc.IdUsuario == idusuario)
                .ToListAsync();
        }

        public async Task FinalizarPedidoAsync(List<int> carrito, int idusuario)
        {
            int idpedido = await GetMaxIdCompraAsync();
            int idfactura = 0;
            DateTime fecha = DateTime.Now;
            foreach (int idlibro in carrito.Distinct())
            {
                await this.context.Pedidos.AddAsync(new Pedido
                {
                    IdPedido = idpedido,
                    IdFactura = idfactura,
                    Fecha = fecha,
                    IdLibro = idlibro,
                    IdUsuario = idusuario,
                    Cantidad = carrito.Count(id => id == idlibro) * (await FindLibroAsync(idlibro)).Precio

                });
                await this.context.SaveChangesAsync();
                idpedido++;
            }
        }
    }
}
