namespace CarteleraBlazor.Server.Helpers
{
    public interface ISaveFile
    {
        Task<string> SaveFile(byte[] data, string extension, string name);
       async Task<string> EditFile(byte[] data, string extension, string name, string ruta)
        {
            if(ruta != null)
            {
                await Eliminar(ruta, name);
            }
           return  await SaveFile(data, extension, name);
        }
        Task Eliminar(string ruta, string name);
    }
}
