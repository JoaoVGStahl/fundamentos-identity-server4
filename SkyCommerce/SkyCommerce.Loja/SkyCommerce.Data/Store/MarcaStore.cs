using Microsoft.EntityFrameworkCore;
using SkyCommerce.Data.Context;
using SkyCommerce.Data.Mappers;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyCommerce.Data.Store
{
    public class MarcaStore : IMarcaStore
    {
        internal DbSet<Entities.Marca> DbSet { get; set; }
        public MarcaStore(SkyContext context)
        {
            this.DbSet = context.Marcas;
        }

        public async Task<IEnumerable<Marca>> ObterTodos()
        {
            var marcas = await DbSet.ToListAsync();
            return marcas.Select(s => s.ToModel());
        }

        public async Task<Marca> ObterPorNome(string marca)
        {
            var marcaDb = await DbSet.FirstOrDefaultAsync(f => f.NomeUnico.Equals(marca));
            return marcaDb.ToModel();
        }
    }
}
