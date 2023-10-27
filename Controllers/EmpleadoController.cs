using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]

public class EmpleadoController : ControllerBase
{
    private readonly EmpleadoContext _context;

    public EmpleadoController(EmpleadoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmpleadoRespuesta>>> ObtenerTodosLosEmpleados()
    {
        List<Empleado>? empleados = await _context.Empleados.ToListAsync();
        
        if (empleados == null )
            return NotFound($"No hay empleados en la base de datos");

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

        return Ok(empleadosRespuesta);
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<ActionResult<EmpleadoRespuesta>> ObtenerEmpleadoPorId(int id)
    {

        if (id < 1)
            return BadRequest($"El id [{id}] no es un id Valido");

            Empleado? empleado = await _context.Empleados.FindAsync(id);
            
            if (empleado == null)
                return NotFound($"No se encontro el empleado con id [{id}]");    

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

        return Ok(empleadoRespuesta);
    }

    [HttpPost]
    public async Task<ActionResult<EmpleadoRespuesta>> AgregarEmpleado([FromBody] EmpleadoNuevoPeticion empleadoNuevoPeticion)
    {
        try
        {
            Empleado nuevoEmpleado = new Empleado()
            {
                Nombre = empleadoNuevoPeticion.Nombre,
                Apellido = empleadoNuevoPeticion.Apellido,
                FechaDeNacimiento = empleadoNuevoPeticion.FechaDeNacimiento,
                TipoDeDocumento = empleadoNuevoPeticion.TipoDeDocumento,
                NumeroDeDocumento = empleadoNuevoPeticion.NumeroDeDocumento,
                MateriaQueDicta = empleadoNuevoPeticion.MateriaQueDicta,
                FechaDeIngreso = empleadoNuevoPeticion.FechaDeIngreso
            };

            await _context.Empleados.AddAsync(nuevoEmpleado);
            await _context.SaveChangesAsync();

            EmpleadoRespuesta empleadoRespuesta = new EmpleadoRespuesta()
            {
                Id = nuevoEmpleado.Id,
                Nombre = nuevoEmpleado.Nombre,
                Apellido = nuevoEmpleado.Apellido, 
                FechaDeNacimiento = nuevoEmpleado.FechaDeNacimiento,
                TipoDeDocumento = nuevoEmpleado.TipoDeDocumento,
                NumeroDeDocumento = nuevoEmpleado.NumeroDeDocumento,
                MateriaQueDicta = nuevoEmpleado.MateriaQueDicta,
                FechaDeIngreso = nuevoEmpleado.FechaDeIngreso,
                FechaDeCese = nuevoEmpleado.FechaDeCese
            };

            return Ok(empleadoRespuesta);
        }
        catch (Exception ex)
        {
            return BadRequest($"Hubo un problema al crear un nuevo empleado: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<ActionResult<EmpleadoRespuesta>> ModificarEmpleado([FromBody] EmpleadoModificacionPeticion nuevoEmpleado)
    { 
        try
        {
            if(nuevoEmpleado.Id < 1)
                return BadRequest($"El id [{nuevoEmpleado.Id}] no es un id valido, este debe ser un entero mayor a 0");

            Empleado? empleado = await _context.Empleados.FindAsync(nuevoEmpleado.Id);

            if (empleado != null)
            {
                empleado.Nombre = nuevoEmpleado.Nombre;
                empleado.Apellido = nuevoEmpleado.Apellido;
                empleado.FechaDeNacimiento = nuevoEmpleado.FechaDeNacimiento;
                empleado.TipoDeDocumento = nuevoEmpleado.TipoDeDocumento;   // validar si ya existe el tipo y el numero documento en la DB
                empleado.NumeroDeDocumento = nuevoEmpleado.NumeroDeDocumento;  
                empleado.MateriaQueDicta = nuevoEmpleado.MateriaQueDicta;
                empleado.FechaDeIngreso = nuevoEmpleado.FechaDeIngreso;
                empleado.FechaDeCese =  nuevoEmpleado.FechaDeCese;

                _context.Empleados.Update(empleado);
                await _context.SaveChangesAsync();

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

                return Ok(empleadoRespuesta);
            }
            else
            {
                return NotFound($"El empleado con Id [{nuevoEmpleado.Id}] no se encuentra en la base de datos");
            } 
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
            if (id < 1)
                return BadRequest($"El id [{id}] no es un id Valido");
            
            Empleado? empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
                return NotFound($"No se encontro el empleado con el id [{id}]");

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

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
            
            return Ok($"Se borro correctamente el empleado: {empleadoRespuesta}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Hubo un error al borrar el empleado: {ex.Message}");
        }
    }
}