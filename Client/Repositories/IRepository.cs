namespace CarteleraBlazor.Client.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>>Get<T>(string url);
        Task<HttpResponseWrapper<object>>Post<T>(string url, T entity);
        Task<HttpResponseWrapper<TResponse>>Post<T,TResponse>(string url,T entity);
    }
}
