using AutoMapper;

namespace SkyCommerce.Data.Mappers
{
    internal static class MarcaMapper
    {
        internal static IMapper Mapper { get; }

        static MarcaMapper()
        {
            Mapper = new MapperConfiguration(m => m.AddProfile<MarcaMapperProfile>()).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.Marca ToModel(this Entities.Marca entity)
        {
            return Mapper.Map<Models.Marca>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Marca ToEntity(this Models.Marca model)
        {
            return Mapper.Map<Entities.Marca>(model);
        }

    }
}