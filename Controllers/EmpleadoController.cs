using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]

public class EmpleadoController : ControllerBase
{
    private readonly IUnidadDeTrabajo _unidadDeTrabajo;
    private readonly AgregarEmpleadoServicio _agregarEmpleadoServicio;
    private readonly ModificarEmpleadoServicio _modificarEmpleadoServicio;
    private readonly BorrarEmpleadoServicio _borrarEmpleadoServicio;

    public EmpleadoController(
            IUnidadDeTrabajo unidadDeTrabajo, 
            AgregarEmpleadoServicio agregarEmpleadoServicio,
            ModificarEmpleadoServicio modificarEmpleadoServicio,
            BorrarEmpleadoServicio borrarEmpleadoServicio
    )
    {
        _unidadDeTrabajo = unidadDeTrabajo;
        _agregarEmpleadoServicio = agregarEmpleadoServicio;
        _modificarEmpleadoServicio = modificarEmpleadoServicio;
        _borrarEmpleadoServicio = borrarEmpleadoServicio;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmpleadoRespuesta>>> ObtenerTodosLosEmpleados()
    {
        List<Empleado>? empleados = await _unidadDeTrabajo.EmpleadoRepositorio.ObtenerTodosLosEmpleados();

        if (empleados == null)
            return NotFound("No hay empleados en la base de datos");

         List<EmpleadoRespuesta> empleadosRespuesta = new List<EmpleadoRespuesta>(); 

        foreach (Empleado e in empleados)
        {
            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta()
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Apellido = e.Apellido, 
                FechaDeNacimiento = e.FechaDeNacimiento,
                TipoDeDocumento = e.TipoDeDocumento,
                NumeroDeDocumento = e.NumeroDeDocumento,
                MateriaQueDicta = e.MateriaQueDicta,
                FechaDeIngreso = e.FechaDeIngreso,
                FechaDeCese = e.FechaDeCese
            };

            empleadosRespuesta.Add(empleadoRespuesta);
        }

        return empleadosRespuesta; 
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<ActionResult<EmpleadoRespuesta>> ObtenerEmpleadoPorId(int id)
    {
        if (id < 1)
            return BadRequest($"El id [{id}] no es un id Valido");

        try
        {
            Empleado? empleado = await _unidadDeTrabajo.EmpleadoRepositorio.ObtenerEmpleadoPorId(id);

            if (empleado == null)
                return NotFound($"El empleado con Id [{id}] no se encuentra en la base de datos");
            
            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta()
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                FechaDeNacimiento = empleado.FechaDeNacimiento,
                TipoDeDocumento = empleado.TipoDeDocumento,
                NumeroDeDocumento = empleado.NumeroDeDocumento,
                MateriaQueDicta = empleado.MateriaQueDicta,
                FechaDeIngreso = empleado.FechaDeIngreso,
                FechaDeCese = empleado.FechaDeCese,
            };

            return Ok(JsonSerializer.Serialize(empleadoRespuesta));
        }
        catch (Exception ex)
        {
            return BadRequest($"Hubo un error al hacer la peticion: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<EmpleadoRespuesta>> AgregarEmpleado([FromBody] EmpleadoNuevoPeticion empleadoNuevoPeticion)
    {
        try
        {
            Empleado? empleado = await _agregarEmpleadoServicio.Ejecutar(empleadoNuevoPeticion);

            if (empleado == null)
                return BadRequest("Valide los datos de ingreso"); 

            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta()
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido, 
                FechaDeNacimiento = empleado.FechaDeNacimiento,
                TipoDeDocumento = empleado.TipoDeDocumento,
                NumeroDeDocumento = empleado.NumeroDeDocumento,
                MateriaQueDicta = empleado.MateriaQueDicta,
                FechaDeIngreso = empleado.FechaDeIngreso,
                FechaDeCese = empleado.FechaDeCese
            };

            return Ok($"Empleado agregado correctamente: {JsonSerializer.Serialize(empleadoRespuesta)}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<EmpleadoRespuesta>> ModificarEmpleado([FromBody] EmpleadoModificacionPeticion nuevoEmpleado)
    { 
        try
        {
            Empleado? empleado = await _modificarEmpleadoServicio.Ejecutar(nuevoEmpleado);

            if (empleado == null)
                return BadRequest("No se pudo modificar el empleado, verifique los datos ingresados");

            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta()
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido, 
                FechaDeNacimiento = empleado.FechaDeNacimiento,
                TipoDeDocumento = empleado.TipoDeDocumento,
                NumeroDeDocumento = empleado.NumeroDeDocumento,
                MateriaQueDicta = empleado.MateriaQueDicta,
                FechaDeIngreso = empleado.FechaDeIngreso,
                FechaDeCese = empleado.FechaDeCese
            };

            return Ok($"Empleado modificado correctamente: {JsonSerializer.Serialize(empleadoRespuesta)}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Peticion echa de forma incorrecta: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<ActionResult<EmpleadoRespuesta>> BorrarEmpleado([FromQuery] int id)
    {
        try
        {
            Empleado? empleado = await _borrarEmpleadoServicio.Ejecutar(id);
            
            if (empleado == null)
                return NotFound($"No se encontro empleado con id {id}");
            
            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta()
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido, 
                FechaDeNacimiento = empleado.FechaDeNacimiento,
                TipoDeDocumento = empleado.TipoDeDocumento,
                NumeroDeDocumento = empleado.NumeroDeDocumento,
                MateriaQueDicta = empleado.MateriaQueDicta,
                FechaDeIngreso = empleado.FechaDeIngreso,
                FechaDeCese = empleado.FechaDeCese
            };

            return Ok($"Se borro correctamente el empleado: {JsonSerializer.Serialize(empleadoRespuesta)}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Hubo un error al borrar el empleado: {ex.Message}");
        }
    }
}