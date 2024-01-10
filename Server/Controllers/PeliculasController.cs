using CarteleraBlazor.Server.Helpers;
using CarteleraBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarteleraBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly DataManager.DataManager dm;
        private readonly ISaveFile saveFile;

        public PeliculasController(DataManager.DataManager dm, ISaveFile saveFile)
        {
            this.dm = dm;
            this.saveFile = saveFile;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Pelicula pelicula)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(pelicula.poster))
                {
                    var fotobyte = Convert.FromBase64String(pelicula.poster);
                    pelicula.poster = await saveFile.SaveFile(fotobyte, ".jpg", "peliculas");
                }

                var id = await dm.guardarPelicula(pelicula);
                return Ok(id);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpGet]
        public async Task<ActionResult<List<Pelicula>>> Get()
        {
            try
            {
                var result = await dm.getPeliculas();
                //result = result.Where(pelicula => pelicula.enCartelera==true).OrderBy(pelicula => pelicula.titulo).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


    }
}
