using AutoMapper;

namespace SkyCommerce.Data.Mappers
{
    internal static class EnderecoMapper
    {
        internal static IMapper Mapper { get; }

        static EnderecoMapper()
        {
            Mapper = new MapperConfiguration(m => m.AddProfile<EnderecoMapperProfile>()).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.Endereco ToModel(this Entities.Endereco entity)
        {
            return Mapper.Map<Models.Endereco>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Endereco ToEntity(this Models.Endereco model)
        {
            return Mapper.Map<Entities.Endereco>(model);
        }

    }
}