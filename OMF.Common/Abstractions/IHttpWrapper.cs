using System.Threading.Tasks;

namespace OMF.Common.Abstractions
{
    public interface IHttpWrapper
    {
        Task<TResponse> Get<TResponse>(string url);
    }
}