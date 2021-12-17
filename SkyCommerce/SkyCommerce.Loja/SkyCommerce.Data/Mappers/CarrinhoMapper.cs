using AutoMapper;

namespace SkyCommerce.Data.Mappers
{
    internal static class CarrinhoMapper
    {
        internal static IMapper Mapper { get; }
        static CarrinhoMapper()
        {
            Mapper = new MapperConfiguration(m =>
            {
                m.AddProfile<CarrinhoMapperProfile>();
                m.AddProfile<ProdutoMapperProfile>();
                m.AddProfile<AvaliacaoMapperProfile>();
                m.AddProfile<MarcaMapperProfile>();
            }).CreateMapper();
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.Carrinho ToModel(this Entities.Carrinho entity)
        {
            return Mapper.Map<Models.Carrinho>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.Carrinho ToEntity(this Models.Carrinho model)
        {
            return Mapper.Map<Entities.Carrinho>(model);
        }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.ItemCarrinho ToModel(this Entities.ItemCarrinho entity)
        {
            return Mapper.Map<Models.ItemCarrinho>(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.ItemCarrinho ToEntity(this Models.ItemCarrinho model)
        {
            return Mapper.Map<Entities.ItemCarrinho>(model);
        }

    }
}