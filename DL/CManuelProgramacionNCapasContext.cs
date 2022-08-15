using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class CManuelProgramacionNCapasContext : DbContext
    {
        public CManuelProgramacionNCapasContext()
        {
        }

        public CManuelProgramacionNCapasContext(DbContextOptions<CManuelProgramacionNCapasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aseguradora> Aseguradoras { get; set; } = null!;
        public virtual DbSet<Colonium> Colonia { get; set; } = null!;
        public virtual DbSet<Dependiente> Dependientes { get; set; } = null!;
        public virtual DbSet<DependienteTipo> DependienteTipos { get; set; } = null!;
        public virtual DbSet<Direccion> Direccions { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<Pai> Pais { get; set; } = null!;
        public virtual DbSet<Poliza> Polizas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<SubPoliza> SubPolizas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database=CManuelProgramacionNCapas; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aseguradora>(entity =>
            {
                entity.HasKey(e => e.IdAseguradora)
                    .HasName("PkIdAseguradora");

                entity.ToTable("Aseguradora");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Aseguradoras)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FkIdUsuario");
            });

            modelBuilder.Entity<Colonium>(entity =>
            {
                entity.HasKey(e => e.IdColonia)
                    .HasName("PK__Colonia__A1580F66B6E58E3B");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Colonia)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FkIdMunicipio");
            });

            modelBuilder.Entity<Dependiente>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Dependiente");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Genero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdDependiente).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rfc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RFC");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDependienteTipoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdDependienteTipo)
                    .HasConstraintName("FkIdDependienteTipo");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FkIdEmpleado");
            });

            modelBuilder.Entity<DependienteTipo>(entity =>
            {
                entity.HasKey(e => e.IdDependienteTipo)
                    .HasName("PkIdDependienteTipo");

                entity.ToTable("DependienteTipo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDireccion)
                    .HasName("PK__Direccio__1F8E0C76C704CA7D");

                entity.ToTable("Direccion");

                entity.Property(e => e.Calle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroExterior)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroInterior)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdColoniaNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdColonia)
                    .HasConstraintName("FkIdColonia");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FkIdUsuarioDireccion");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PkIdEmpleado");

                entity.ToTable("Empleado");

                entity.HasIndex(e => e.Email, "UQ__Empleado__A9D1053437193BEF")
                    .IsUnique();

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Foto).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nss)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NSS");

                entity.Property(e => e.NumeroEmpleado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rfc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RFC");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FkIdEmpresa");

                entity.HasOne(d => d.IdPolizaNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdPoliza)
                    .HasConstraintName("FkIdPoliza");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PkIdEmpresa");

                entity.ToTable("Empresa");

                entity.Property(e => e.DireccionWeb)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.Logo).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__Estado__FBB0EDC15C7F52C7");

                entity.ToTable("Estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Estados)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FkIdPais");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PK__Municipi__610059788329A03A");

                entity.ToTable("Municipio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("FkIdEstado");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.HasKey(e => e.IdPais)
                    .HasName("PK__Pais__FC850A7B8D6E3871");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.HasKey(e => e.IdPoliza)
                    .HasName("PkIdPoliza");

                entity.ToTable("Poliza");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPoliza)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSubPolizaNavigation)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdSubPoliza)
                    .HasConstraintName("FkIdSubPoliza");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FkIdUsuarioPoliza");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C0A4A4D2A");

                entity.ToTable("Rol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubPoliza>(entity =>
            {
                entity.HasKey(e => e.IdSubPoliza)
                    .HasName("PkIdSubPoliza");

                entity.ToTable("SubPoliza");

                entity.Property(e => e.IdSubPoliza).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PkIdUsuario");

                entity.ToTable("Usuario");

                entity.HasIndex(e => e.UserName, "UcUserName")
                    .IsUnique();

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Curp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Imagen).IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FkIdRol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
