using CarteleraBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarteleraBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly DataManager.DataManager dm;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public GenerosController(DataManager.DataManager dm, IConfiguration configuration, IWebHostEnvironment env)
        {
            this.dm = dm;
            this.configuration = configuration;
            this.env = env;
        }
        [HttpPost]
        public async Task<ActionResult> Post(Genero genero)
        {
            try
            {
                await dm.guardarGenero(genero);
                return Ok("Datos guadados satisfactoriamente");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }

        [HttpGet]
        public async Task<ActionResult<List<Genero>>> Getgeneros()
        {
            try
            {
                var result = await dm.getGeneros();
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }
        [HttpGet("GetGeneroById")]
        public async Task<ActionResult<Genero>> GetGeneroById(string id)
        {
            try
            {
                var result = await dm.getGenero(id);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }

    }
}
