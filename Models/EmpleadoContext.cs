using Microsoft.EntityFrameworkCore;

public partial class EmpleadoContext : DbContext
{
    public EmpleadoContext() {}
    
    public EmpleadoContext(DbContextOptions<EmpleadoContext> options)
        :base(options) {}

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_empleado");

            entity.ToTable("empleados");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()  // id autoincremental
                .HasColumnName("id");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            
            entity.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido");
            
            entity.Property(e => e.FechaDeNacimiento)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("fecha_nacimiento");
            
            entity.Property(e => e.TipoDeDocumento)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tipo_doc");
            
            entity.Property(e => e.NumeroDeDocumento)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_documento");
            
            entity.Property(e => e.MateriaQueDicta)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("materia");
            
            entity.Property(e => e.FechaDeIngreso)
                .IsRequired()
                .HasColumnName("fecha_ingreso");
            
            entity.Property(e => e.FechaDeCese)
                .HasColumnName("fecha_cese");
        });
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}