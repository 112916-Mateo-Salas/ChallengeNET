using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserChallenge.Application.Repositories;
using UserChallenge.Application.Services.Interface;
using UserChallenge.Domain.Model;

namespace UserChallenge.Application.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _articuloRepository;

        public ArticuloService(IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
        }

        public bool DeleteArticulo(int articuloId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Articulo>> GetAllArticulos()
        {
            List<Articulo> articulos = await _articuloRepository.GetAllArticulos();
            return articulos;
        }

        Articulo IArticuloService.GetArticulo(string nombre, string marca)
        {
            throw new NotImplementedException();
        }

        Articulo IArticuloService.GetArticuloById(int id)
        {
            throw new NotImplementedException();
        }

        int IArticuloService.GetArticuloCount(Categoria categoria, Marca marca, string nombre, decimal precio)
        {
            throw new NotImplementedException();
        }

        List<Articulo> IArticuloService.GetArticulosByCategoria(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        List<Articulo> IArticuloService.GetArticulosByfilters(string naombre, Categoria categoria, string marca, decimal precio)
        {
            throw new NotImplementedException();
        }

        void IArticuloService.UpdateArticulo(Articulo articulo, int articuloId)
        {
            throw new NotImplementedException();
        }
    }
}
