using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Domain.Model;

namespace UserChallenge.Application.Repositories
{
    public interface IArticuloRepository
    {
        Task<List<Articulo>> GetAllArticulos();

        Task<Articulo> GetArticuloById(int id);

        Task<Articulo> GetArticulo(string nombre, string marca);

        Task<List<Articulo>> GetArticulosByfilters(string naombre, Categoria categoria, string marca, decimal? precio);

        Task UpdateArticulo(Articulo articulo);

        Task DeleteArticulo(Articulo articulo);

        Task<List<Articulo>> GetArticulosByCategoria(Categoria categoria);

        Task<int> GetArticuloCount(Categoria categoria, Marca marca, string nombre, decimal? precio);
    }
}
