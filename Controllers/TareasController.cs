using Microsoft.AspNetCore.Mvc;

namespace espTareas.Controllers;

[ApiController]
[Route("[controller]")]
public class TareasController : ControllerBase
{

    private readonly ILogger<TareasController> _logger;
    private AccesoADatos Acceso;
    private ManejoDeTareas Manejo;

    public TareasController(ILogger<TareasController> logger)
    {
        _logger = logger;
        Acceso = new AccesoADatos();
        Manejo = new ManejoDeTareas(Acceso);

        Manejo.CargarTareas();
    }

    [HttpPost("AgregarTarea")]
    public ActionResult<string> AddTarea(Tareas Tarea)
    {
       if(Manejo.AgregarTarea(Tarea)) return Ok("Tarea agregada con éxito");
       
       return BadRequest();
    }

    [HttpGet("BuscarTarea/{Id}")]
    public ActionResult<Tareas> BuscarTarea(int Id)
    {
        var Tar = Manejo.BuscarTarea(Id);

       if(Tar != null)
       {
        return Ok(Tar);
       }

       return NotFound();
    }

    [HttpPut("ActualizarTarea")]
    public ActionResult<string> ActualizarTarea(Tareas Tar)
    {
        if(Manejo.ActualizarTarea(Tar)) return Ok("Tarea actualizada con exito");

        return Ok("No se pudo actualizar la tarea");
    }

    [HttpDelete("EliminarTarea")]
    public ActionResult<string> EliminarTarea(int Id)
    {
        if(Manejo.EliminarTarea(Id)) return Ok("Tarea eliminada con exito");

        return NotFound("No se encontró la tarea");
    }

    [HttpGet("ListarTareas")]
    public ActionResult<IEnumerable<Tareas>> ListarTareas()
    {
        return Ok(Manejo.MostrarTareas());
    }

    [HttpGet("ListarTareasCompletadas")]
    public ActionResult<IEnumerable<Tareas>> ListarTareasCompletadas()
    {
        return Ok(Manejo.MostrarTareasCompletadas());
    }

}
