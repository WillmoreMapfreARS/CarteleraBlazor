using System.Text;
using System.Text.Json;

namespace CarteleraBlazor.Client.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient httpClient;
        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private async Task<T> Deserializar<T>(HttpResponseMessage httpResponse, JsonSerializerOptions serializerOptions)
        {
            try
            {
                var response = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(response, serializerOptions);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public Repository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
           var responseHTTP = await httpClient.GetAsync(url);
            if (responseHTTP.IsSuccessStatusCode)
            {
                var result= await Deserializar<T>(responseHTTP, jsonSerializerOptions);
                return new HttpResponseWrapper<T>(result, false, responseHTTP);
            }
            return new HttpResponseWrapper<T>(default, true, responseHTTP);

        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T entity)
        {
            var dataJson = JsonSerializer.Serialize(entity);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, dataContent);
            return new HttpResponseWrapper<object>(null,!response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T entity)
        {
            var dataJson = JsonSerializer.Serialize(entity);
            var dataContent = new StringContent(dataJson, encoding: Encoding.UTF8, "application/json");
            var respose = await httpClient.PostAsync(url, dataContent);
            if(respose.IsSuccessStatusCode)
            {
                var result= await Deserializar<TResponse>(respose, jsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(result, false, respose);
            }
            return  new HttpResponseWrapper<TResponse>(default, true, respose);
        }
    }
}
