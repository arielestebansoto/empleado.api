public interface IUnidadDeTrabajo : IDisposable
{
    public IEmpleadoRepositorio EmpleadoRepositorio { get; }
    Task<int> Guardar();
}