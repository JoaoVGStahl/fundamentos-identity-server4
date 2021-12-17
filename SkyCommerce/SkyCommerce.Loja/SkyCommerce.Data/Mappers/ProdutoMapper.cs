using AutoMapper;

namespace SkyCommerce.Data.Mappers
{
    internal static class ProdutoMapper
    {
        internal static IMapper Mapper { get; }
        static ProdutoMapper()
        {
            Mapper = new MapperConfiguration(m =>
                {
                    m.AddProfile<ProdutoMapperProfile>();
                    m.AddProfile<AvaliacaoMapperProfile>();
                    m.AddProfile<MarcaMapperProfile>();
                }
            ).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.Produto ToModel(this Entities.Produto entity)
        {
            return Mapper.Map<Models.Produto>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Produto ToEntity(this Models.Produto model)
        {
            return Mapper.Map<Entities.Produto>(model);
        }

    }
}
