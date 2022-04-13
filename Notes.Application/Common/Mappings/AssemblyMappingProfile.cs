using AutoMapper;
using System.Reflection;
using System.Linq;


namespace Notes.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingFromAssebly(assembly);

        // сканирует сборку и ищет любые типы, которые реализует IMapWith
        private void ApplyMappingFromAssebly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            // вызывает метод mapping от наследуемого типа или интерфейса, если тип не реализует
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] {this});

            }
        }
    }
}
