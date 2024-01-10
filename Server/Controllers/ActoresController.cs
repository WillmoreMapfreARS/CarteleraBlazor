using CarteleraBlazor.Server.Helpers;
using CarteleraBlazor.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace CarteleraBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private readonly DataManager.DataManager dm;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;
        private readonly ISaveFile saveFile;

        public ActoresController(DataManager.DataManager dm, IConfiguration configuration, IWebHostEnvironment env, ISaveFile saveFile)
        {
            this.dm = dm;
            this.configuration = configuration;
            this.env = env;
            this.saveFile = saveFile;
        }
        [HttpPost]
        public async Task<ActionResult> Post(Actor actor)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(actor.foto))
                {
                    try
                    {
                        var fotobyte = Convert.FromBase64String(actor.foto);
                        actor.foto = await saveFile.SaveFile(fotobyte, ".jpg", "personas");
                    }
                    catch (Exception e)
                    {


                    }

                }

                await dm.guardarActor(actor);
                return Ok("Datos guadados satisfactoriamente");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }
        [HttpGet]
        public async Task<ActionResult<List<Actor>>> GetActores()
        {
            try
            {
                var result = await dm.getActores();
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }
        [HttpGet("buscar/{texto}")]
        public async Task<ActionResult<List<Actor>>> GetActoresFilter(string texto)
        {
            try
            {
                var result = await dm.getActores();
                result = result.Where(x => x.nombre.ToLower().Contains(texto.ToLower())).Take(5).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }
        [HttpGet("GetActorById/{id}")]
        public async Task<ActionResult<Actor>> GetActorById(string id)
        {
            try
            {
                var result = await dm.getActor(id);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }
    }
}
