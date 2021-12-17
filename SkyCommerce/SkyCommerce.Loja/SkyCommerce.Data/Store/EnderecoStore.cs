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
    public class EnderecoStore : IEnderecoStore
    {
        internal DbSet<Entities.Endereco> DbSet { get; set; }
        public EnderecoStore(SkyContext context)
        {
            this.DbSet = context.Enderecos;
        }


        public async Task<IEnumerable<Endereco>> ObterDoUsuario(string usuario)
        {
            var enderecos = await DbSet.Where(w => w.Usuario == usuario).ToListAsync();
            return enderecos.Select(s => s.ToModel());
        }
    }
}
