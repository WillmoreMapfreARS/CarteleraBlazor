namespace CarteleraBlazor.Server.Helpers
{
    public class SaveLocalFile : ISaveFile
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SaveLocalFile( IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Task Eliminar(string ruta, string folder)
        {
            var nombreArchivo= Path.GetFileName(ruta);
            var directorioArchivo= Path.Combine(env.WebRootPath,folder, nombreArchivo);
            if(File.Exists(directorioArchivo))
            {
                File.Delete(directorioArchivo);
            }
            return Task.CompletedTask;
        }

        public async Task<string> SaveFile(byte[] data, string extension, string folder)
        {
           var nombreArchivo= $"{ Guid.NewGuid().ToString()}{extension}"; 
            var contenedor= Path.Combine(env.WebRootPath, folder);
            if(!Directory.Exists(contenedor))
            {
                Directory.CreateDirectory(contenedor);
            }
            string destinationPath = Path.Combine(contenedor, nombreArchivo);
            await File.WriteAllBytesAsync(destinationPath,data);
            var urlActual = $"{httpContextAccessor!.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var ruta= Path.Combine(urlActual, folder,nombreArchivo).Replace("\\","/");
            return ruta ;
        }
    }
}
