using Microsoft.EntityFrameworkCore;
using SkyCommerce.Data.Context;
using SkyCommerce.Data.Mappers;
using SkyCommerce.Data.Util;
using SkyCommerce.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyCommerce.Data.Store
{
    public class CategoriaStore : ICategoriaStore
    {
        private SkyContext Db { get; set; }
        private DbSet<Entities.Categoria> DbSet { get; set; }

        public CategoriaStore(SkyContext context)
        {
            this.Db = context;
            this.DbSet = context.Categorias;
        }



        public async Task<List<Models.Categoria>> ObterTodos()
        {
            var entity = await DbSet.OrderBy(b => b.Nome).ToListAsync();

            return entity.Select(s => s.ToModel()).ToList();
        }

        public async Task<Models.Categoria> ObterPorNome(string nomeUnico)
        {
            var entity = await Obter(nomeUnico);
            return entity.ToModel();
        }


        private async Task<Entities.Categoria> Obter(string nomeUnico)
        {
            return await DbSet.FirstOrDefaultAsync(f => f.NomeUnico.ToLower().Equals(nomeUnico.ToLower()));
        }

        public async Task Adicionar(Models.Categoria obj)
        {
            await DbSet.AddAsync(obj.ToEntity());
            await Db.SaveChangesAsync();
        }

        public async Task Atualizar(Models.Categoria obj)
        {
            var entityDb = await Obter(obj.NomeUnico);
            var entity = obj.ToEntity();

            entity.Id = entityDb.Id;

            entity.ShallowCopyTo(entityDb);
            await Db.SaveChangesAsync();
        }

        public async Task Remover(Models.Categoria obj)
        {
            var paraRemover = await Obter(obj.NomeUnico);
            DbSet.Remove(paraRemover);
            await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
        }

    }
}