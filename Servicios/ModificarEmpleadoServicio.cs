public class ModificarEmpleadoServicio
{
    private readonly IUnidadDeTrabajo _unidadDeTrabajo;

    public ModificarEmpleadoServicio(IUnidadDeTrabajo unidadDeTrabajo)
    {
        _unidadDeTrabajo = unidadDeTrabajo; 
    }

     public async Task<Empleado?> Ejecutar(EmpleadoModificacionPeticion nuevoEmpleado)
     {
        try
        {
            Empleado? empleado = await _unidadDeTrabajo.EmpleadoRepositorio.ObtenerEmpleadoPorId(nuevoEmpleado.Id);
            
            if (empleado == null)
                return null;

            empleado.Id = nuevoEmpleado.Id ;
            empleado.Nombre = nuevoEmpleado.Nombre ;
            empleado.Apellido = nuevoEmpleado.Apellido ; 
            empleado.FechaDeNacimiento = nuevoEmpleado.FechaDeNacimiento ; 
            empleado.TipoDeDocumento = nuevoEmpleado.TipoDeDocumento ;
            empleado.NumeroDeDocumento = nuevoEmpleado.NumeroDeDocumento ;
            empleado.MateriaQueDicta = nuevoEmpleado.MateriaQueDicta; 
            empleado.FechaDeIngreso = nuevoEmpleado.FechaDeIngreso ;
            empleado.FechaDeCese = nuevoEmpleado.FechaDeCese ;

            _unidadDeTrabajo.EmpleadoRepositorio.ModificarEmpleado(empleado);

            await _unidadDeTrabajo.Guardar();
            
            return empleado;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al modidicar el empleado: {ex.Message}");
        }
     }
}