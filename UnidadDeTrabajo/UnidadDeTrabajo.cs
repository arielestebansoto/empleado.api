public class UnidadDeTrabajo : IUnidadDeTrabajo
{
    private readonly EmpleadoContext _context;
    public IEmpleadoRepositorio EmpleadoRepositorio { get; }

    public UnidadDeTrabajo(EmpleadoContext context, IEmpleadoRepositorio empleadoRepositorio)
    {
        _context = context;
        EmpleadoRepositorio = empleadoRepositorio;
    }

    public async Task<int> Guardar()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}