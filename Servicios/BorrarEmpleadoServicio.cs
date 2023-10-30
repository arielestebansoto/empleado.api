public class BorrarEmpleadoServicio
{
    private readonly IUnidadDeTrabajo _unidadDeTrabajo;

    public BorrarEmpleadoServicio(IUnidadDeTrabajo unidadDeTrabajo)
    {
        _unidadDeTrabajo = unidadDeTrabajo; 
    }

     public async Task<Empleado?> Ejecutar(int id)
     {
        Empleado? empleado = await _unidadDeTrabajo.EmpleadoRepositorio.ObtenerEmpleadoPorId(id);
        
        if (empleado == null)
            return null;

        _unidadDeTrabajo.EmpleadoRepositorio.BorrarEmpleado(empleado);

        await _unidadDeTrabajo.Guardar();
        
        return empleado;
     }
}