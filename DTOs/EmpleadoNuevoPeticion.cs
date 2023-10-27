public class EmpleadoNuevoPeticion
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!; 
    public DateTime FechaDeNacimiento { get; set; }
    public string TipoDeDocumento { get; set; } = null!;
    public string NumeroDeDocumento { get; set; } = null!;
    public string MateriaQueDicta { get; set; } = null!; 
    public DateTime FechaDeIngreso { get; set; }
 }