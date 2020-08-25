using AutoMapper;

namespace ACI.Application.Identity.Services.Helpers
{
    public static class GenericMapper<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        public static TDto ToDto(TEntity entity)
        {
            var config = new MapperConfiguration(x => x.CreateMap<TEntity, TDto>());
            var mapper = new Mapper(config);
            var dto = mapper.Map<TEntity, TDto>(entity);
            return dto;
        }
    }
}
