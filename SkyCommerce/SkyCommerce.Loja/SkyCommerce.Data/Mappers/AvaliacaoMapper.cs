using AutoMapper;

namespace SkyCommerce.Data.Mappers
{
    internal static class AvaliacaoMapper
    {
        internal static IMapper Mapper { get; }

        static AvaliacaoMapper()
        {
            Mapper = new MapperConfiguration(m => m.AddProfile<AvaliacaoMapperProfile>()).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.Avaliacao ToModel(this Entities.Avaliacao entity)
        {
            return Mapper.Map<Models.Avaliacao>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Avaliacao ToEntity(this Models.Avaliacao model)
        {
            return Mapper.Map<Entities.Avaliacao>(model);
        }

    }
}