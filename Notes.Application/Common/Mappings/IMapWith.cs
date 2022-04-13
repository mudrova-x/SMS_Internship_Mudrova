using AutoMapper;

namespace Notes.Application.Common.Mappings
{
    public interface IMapWith<T>
    {
        // создает конфигурацию из исходного типа Т
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
