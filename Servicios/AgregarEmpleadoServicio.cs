public class AgregarEmpleadoServicio
{
    private readonly IUnidadDeTrabajo _unidadDeTrabajo;

    public AgregarEmpleadoServicio(IUnidadDeTrabajo unidadDeTrabajo)
    {
        _unidadDeTrabajo = unidadDeTrabajo; 
    }

     public async Task<Empleado?> Ejecutar(EmpleadoNuevoPeticion empleadoNuevoPeticion)
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
        
        Empleado? empleado = await _unidadDeTrabajo.EmpleadoRepositorio.AgregarEmpleado(nuevoEmpleado);
        
        if (empleado == null)
            return null;

        await _unidadDeTrabajo.Guardar();
        
        return empleado;
     }
}