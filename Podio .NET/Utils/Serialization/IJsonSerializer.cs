
namespace PodioAPI.Utils
{
    public interface IJsonSerializer
    {
        string Serilaize(object entity);
        object Deserilaize<T>(string json);
    }
}
