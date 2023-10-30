using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EmpleadoRepositorio : IEmpleadoRepositorio
{
    private readonly EmpleadoContext _context;
    public EmpleadoRepositorio(EmpleadoContext context)
    {
        _context = context;
    }
    
    public async Task<List<Empleado>?> ObtenerTodosLosEmpleados()
    {       
        List<Empleado>? empleados = await _context.Empleados.ToListAsync();
        
        if (empleados == null)
            return null;
        
        return empleados;
    }

    public async Task<Empleado?> ObtenerEmpleadoPorId(int id)
    {         
        Empleado? empleado = await _context.Empleados.FindAsync(id);
            
        if (empleado == null)
            return null;    
        
        return empleado;
    }
    
    public async Task<Empleado?> AgregarEmpleado(Empleado empleado)
    {
        try
        {
            await _context.Empleados.AddAsync(empleado);
            
            return empleado;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void ModificarEmpleado(Empleado empleado)
    {
        _context.Empleados.Update(empleado);
    }

    public void BorrarEmpleado(Empleado empleado)
    {
         _context.Empleados.Remove(empleado);
    }

    public void ActualizarEmpleado(Empleado empleado)
    {
        //_context.Entry(empleado).State = EntityState.Modified;
        _context.Empleados.Update(empleado);
    }
}