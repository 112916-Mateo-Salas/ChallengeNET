using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Domain.Model;

namespace UserChallenge.Application.Services.Interface
{
    public interface IArticuloService
    {
        Task<List<Articulo>> GetAllArticulos();

        Articulo GetArticuloById (int id);

        Articulo GetArticulo(string nombre, string marca);

        List<Articulo> GetArticulosByfilters(string naombre, Categoria categoria, string marca, decimal precio);

        void UpdateArticulo(Articulo articulo, int articuloId);

        bool DeleteArticulo(int articuloId);

        List<Articulo> GetArticulosByCategoria( Categoria categoria);

        int GetArticuloCount(Categoria categoria, Marca marca, string nombre, decimal precio);


    }
}
