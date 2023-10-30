using Microsoft.AspNetCore.Mvc;

public interface IEmpleadoRepositorio
{
    Task<List<Empleado>?> ObtenerTodosLosEmpleados();
    Task<Empleado?> ObtenerEmpleadoPorId(int id);
    Task<Empleado?> AgregarEmpleado(Empleado empleado);
    void ModificarEmpleado(Empleado empleado);
    void BorrarEmpleado(Empleado empleado);
    void ActualizarEmpleado(Empleado empleado);
}