namespace CarteleraBlazor.Client.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage) {
            Response = response;
            hasError = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; set; }
        public bool hasError { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public async Task<string?> getError()
        {
            if(hasError)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
