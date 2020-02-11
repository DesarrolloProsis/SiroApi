using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Models
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Carriles> Carriles { get; set; }
        public virtual DbSet<ConcentradoTransacciones> ConcentradoTransacciones { get; set; }
        public virtual DbSet<Delegaciones> Delegaciones { get; set; }
        public virtual DbSet<EficienciasCarriles> EficienciasCarriles { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Operadores> Operadores { get; set; }
        public virtual DbSet<OperadorPlaza> OperadorPlaza { get; set; }
        public virtual DbSet<Plazas> Plazas { get; set; }
        public virtual DbSet<Puestos> Puestos { get; set; }
        public virtual DbSet<TipoCarril> TipoCarril { get; set; }
        public virtual DbSet<TipoPago> TipoPago { get; set; }
        public virtual DbSet<TipoVehiculo> TipoVehiculo { get; set; }
        public virtual DbSet<Tramos> Tramos { get; set; }
        public virtual DbSet<Transacciones> Transacciones { get; set; }
        public virtual DbSet<Turnos> Turnos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = 192.168.0.88; Initial Catalog = CLR_2; User ID = sa; password = CAPUFE; MultipleActiveResultSets = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carriles>(entity =>
            {
                entity.HasKey(e => new { e.NumeroCapufeCarril, e.IdGare });

                entity.HasIndex(e => e.IdGare)
                    .HasName("IX_IdGare");

                entity.HasIndex(e => e.TipoCarrilId)
                    .HasName("IX_TipoCarrilId");

                entity.Property(e => e.CamaraCabina1).HasMaxLength(10);

                entity.Property(e => e.CamaraCabina2).HasMaxLength(10);

                entity.Property(e => e.CamaraCarril).HasMaxLength(10);

                entity.Property(e => e.FechaMejorDia).HasColumnType("date");

                entity.Property(e => e.LineaCarril).HasMaxLength(5);

                entity.Property(e => e.TipoCarrilId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.IdGareNavigation)
                    .WithMany(p => p.Carriles)
                    .HasForeignKey(d => d.IdGare)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Carriles_dbo.Tramos_IdGare");

                entity.HasOne(d => d.TipoCarril)
                    .WithMany(p => p.Carriles)
                    .HasForeignKey(d => d.TipoCarrilId)
                    .HasConstraintName("FK_dbo.Carriles_dbo.TipoCarril_TipoCarrilId");
            });

            modelBuilder.Entity<ConcentradoTransacciones>(entity =>
            {
                entity.HasKey(e => new { e.NumeroCapufeCarril, e.Fecha, e.IdGare, e.TipoPagoId, e.TipoVehiculoId, e.TurnoId });

                entity.HasIndex(e => e.TipoPagoId)
                    .HasName("IX_TipoPagoId");

                entity.HasIndex(e => e.TipoVehiculoId)
                    .HasName("IX_TipoVehiculoId");

                entity.HasIndex(e => e.TurnoId)
                    .HasName("IX_TurnoId");

                entity.HasIndex(e => new { e.NumeroCapufeCarril, e.IdGare })
                    .HasName("IX_NumeroCapufeCarril_IdGare");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdGareNavigation)
                    .WithMany(p => p.ConcentradoTransacciones)
                    .HasForeignKey(d => d.IdGare)
                    .HasConstraintName("FK_dbo.ConcentradoTransacciones_dbo.Tramos_IdGare");

                entity.HasOne(d => d.TipoPago)
                    .WithMany(p => p.ConcentradoTransacciones)
                    .HasForeignKey(d => d.TipoPagoId)
                    .HasConstraintName("FK_dbo.ConcentradoTransacciones_dbo.TipoPago_TipoPagoId");

                entity.HasOne(d => d.TipoVehiculo)
                    .WithMany(p => p.ConcentradoTransacciones)
                    .HasForeignKey(d => d.TipoVehiculoId)
                    .HasConstraintName("FK_dbo.ConcentradoTransacciones_dbo.TipoVehiculo_TipoVehiculoId");

                entity.HasOne(d => d.Turno)
                    .WithMany(p => p.ConcentradoTransacciones)
                    .HasForeignKey(d => d.TurnoId)
                    .HasConstraintName("FK_dbo.ConcentradoTransacciones_dbo.Turnos_TurnoId");

                entity.HasOne(d => d.Carriles)
                    .WithMany(p => p.ConcentradoTransacciones)
                    .HasForeignKey(d => new { d.NumeroCapufeCarril, d.IdGare })
                    .HasConstraintName("FK_dbo.ConcentradoTransacciones_dbo.Carriles_NumeroCapufeCarril_IdGare");
            });

            modelBuilder.Entity<Delegaciones>(entity =>
            {
                entity.HasKey(e => e.DelegacionId);

                entity.Property(e => e.DelegacionId)
                    .HasMaxLength(3)
                    .ValueGeneratedNever();

                entity.Property(e => e.NombreDelegacion).HasMaxLength(50);
            });

            modelBuilder.Entity<EficienciasCarriles>(entity =>
            {
                entity.HasKey(e => new { e.NumeroCapufeCarril, e.IdGare, e.TipoCarrilId, e.Fecha });

                entity.HasIndex(e => e.TipoCarrilId)
                    .HasName("IX_TipoCarrilId");

                entity.HasIndex(e => new { e.NumeroCapufeCarril, e.IdGare })
                    .HasName("IX_NumeroCapufeCarril_IdGare");

                entity.Property(e => e.TipoCarrilId).HasMaxLength(128);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.EficienciaDiaPost).HasColumnName("EficienciaDiaPOST");

                entity.Property(e => e.EficienciaDiaPre).HasColumnName("EficienciaDiaPRE");

                entity.Property(e => e.EficienciaMatutinoPost).HasColumnName("EficienciaMatutinoPOST");

                entity.Property(e => e.EficienciaMatutinoPre).HasColumnName("EficienciaMatutinoPRE");

                entity.Property(e => e.EficienciaNocturnoPost).HasColumnName("EficienciaNocturnoPOST");

                entity.Property(e => e.EficienciaNocturnoPre).HasColumnName("EficienciaNocturnoPRE");

                entity.Property(e => e.EficienciaVespertinoPost).HasColumnName("EficienciaVespertinoPOST");

                entity.Property(e => e.EficienciaVespertinoPre).HasColumnName("EficienciaVespertinoPRE");

                entity.HasOne(d => d.TipoCarril)
                    .WithMany(p => p.EficienciasCarriles)
                    .HasForeignKey(d => d.TipoCarrilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.EficienciasCarriles_dbo.TipoCarril_TipoCarrilId");

                entity.HasOne(d => d.Carriles)
                    .WithMany(p => p.EficienciasCarriles)
                    .HasForeignKey(d => new { d.NumeroCapufeCarril, d.IdGare })
                    .HasConstraintName("FK_dbo.EficienciasCarriles_dbo.Carriles_NumeroCapufeCarril_IdGare");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Operadores>(entity =>
            {
                entity.HasKey(e => e.NumeroCapufeOperador);

                entity.HasIndex(e => e.PuestoId)
                    .HasName("IX_PuestoId");

                entity.Property(e => e.NumeroCapufeOperador)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.ApellidoMaterno).HasMaxLength(50);

                entity.Property(e => e.ApellidoPaterno).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Psd)
                    .HasColumnName("PSD")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Puesto)
                    .WithMany(p => p.Operadores)
                    .HasForeignKey(d => d.PuestoId)
                    .HasConstraintName("FK_dbo.Operadores_dbo.Puestos_PuestoId");
            });

            modelBuilder.Entity<OperadorPlaza>(entity =>
            {
                entity.HasKey(e => new { e.NumeroCapufeOperador, e.NumeroGea, e.NumeroPlazaCapufe });

                entity.HasIndex(e => e.NumeroCapufeOperador)
                    .HasName("IX_NumeroCapufeOperador");

                entity.HasIndex(e => e.NumeroPlazaCapufe)
                    .HasName("IX_NumeroPlazaCapufe");

                entity.Property(e => e.NumeroCapufeOperador).HasMaxLength(128);

                entity.Property(e => e.NumeroGea).HasMaxLength(8);

                entity.Property(e => e.NumeroPlazaCapufe).HasMaxLength(128);

                entity.HasOne(d => d.NumeroCapufeOperadorNavigation)
                    .WithMany(p => p.OperadorPlaza)
                    .HasForeignKey(d => d.NumeroCapufeOperador)
                    .HasConstraintName("FK_dbo.OperadorPlaza_dbo.Operadores_NumeroCapufeOperador");

                entity.HasOne(d => d.NumeroPlazaCapufeNavigation)
                    .WithMany(p => p.OperadorPlaza)
                    .HasForeignKey(d => d.NumeroPlazaCapufe)
                    .HasConstraintName("FK_dbo.OperadorPlaza_dbo.Plazas_NumeroPlazaCapufe");
            });

            modelBuilder.Entity<Plazas>(entity =>
            {
                entity.HasKey(e => e.NumeroPlazaCapufe);

                entity.HasIndex(e => e.DelegacionId)
                    .HasName("IX_DelegacionId");

                entity.Property(e => e.NumeroPlazaCapufe)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.DelegacionId)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.IpServidor).HasMaxLength(14);

                entity.Property(e => e.NombrePlaza).HasMaxLength(60);

                entity.HasOne(d => d.Delegacion)
                    .WithMany(p => p.Plazas)
                    .HasForeignKey(d => d.DelegacionId)
                    .HasConstraintName("FK_dbo.Plazas_dbo.Delegaciones_DelegacionId");
            });

            modelBuilder.Entity<Puestos>(entity =>
            {
                entity.HasKey(e => e.PuestoId);

                entity.Property(e => e.NombrePuesto).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoCarril>(entity =>
            {
                entity.Property(e => e.TipoCarrilId)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasMaxLength(40);

                entity.Property(e => e.Tipo).HasMaxLength(5);
            });

            modelBuilder.Entity<TipoPago>(entity =>
            {
                entity.Property(e => e.TipoPagoId).ValueGeneratedNever();

                entity.Property(e => e.NombrePago).HasMaxLength(40);
            });

            modelBuilder.Entity<TipoVehiculo>(entity =>
            {
                entity.Property(e => e.TipoVehiculoId).ValueGeneratedNever();

                entity.Property(e => e.ClaveVehiculo).HasMaxLength(20);
            });

            modelBuilder.Entity<Tramos>(entity =>
            {
                entity.HasKey(e => e.IdGare);

                entity.HasIndex(e => e.NumeroPlazaCapufe)
                    .HasName("IX_NumeroPlazaCapufe");

                entity.Property(e => e.IdGare).ValueGeneratedNever();

                entity.Property(e => e.IpVideo).HasMaxLength(14);

                entity.Property(e => e.NombreTramo).HasMaxLength(30);

                entity.Property(e => e.NumeroPlazaCapufe)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.NumeroPlazaCapufeNavigation)
                    .WithMany(p => p.Tramos)
                    .HasForeignKey(d => d.NumeroPlazaCapufe)
                    .HasConstraintName("FK_dbo.Tramos_dbo.Plazas_NumeroPlazaCapufe");
            });

            modelBuilder.Entity<Transacciones>(entity =>
            {
                entity.HasKey(e => e.TransaccionId);

                entity.HasIndex(e => e.TipoPagoId)
                    .HasName("IX_TipoPagoId");

                entity.HasIndex(e => e.TurnoId)
                    .HasName("IX_TurnoId");

                entity.HasIndex(e => new { e.NumeroCapufeCarril, e.IdGare })
                    .HasName("IX_NumeroCapufeCarril_IdGare");

                entity.Property(e => e.Cajero).HasMaxLength(5);

                entity.Property(e => e.Comentarios).HasMaxLength(500);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FechaAperturaTurno).HasColumnType("date");

                entity.Property(e => e.Hora).HasMaxLength(15);

                entity.Property(e => e.HoraInicioTurno).HasMaxLength(15);

                entity.Property(e => e.LineaCarril).HasMaxLength(5);

                entity.Property(e => e.NumIave)
                    .HasColumnName("NumIAVE")
                    .HasMaxLength(100);

                entity.Property(e => e.NumeroGea).HasMaxLength(8);

                entity.Property(e => e.ObservacionesMp)
                    .HasColumnName("ObservacionesMP")
                    .HasMaxLength(5);

                entity.Property(e => e.ObservacionesPaso).HasMaxLength(5);

                entity.Property(e => e.ObservacionesSecuencia).HasMaxLength(5);

                entity.Property(e => e.ObservacionesTt)
                    .HasColumnName("ObservacionesTT")
                    .HasMaxLength(5);

                entity.Property(e => e.Post)
                    .HasColumnName("POST")
                    .HasMaxLength(5);

                entity.Property(e => e.Pre)
                    .HasColumnName("PRE")
                    .HasMaxLength(5);

                entity.Property(e => e.TipoPagoDesc).HasMaxLength(5);

                entity.HasOne(d => d.IdGareNavigation)
                    .WithMany(p => p.Transacciones)
                    .HasForeignKey(d => d.IdGare)
                    .HasConstraintName("FK_dbo.Transacciones_dbo.Tramos_IdGare");

                entity.HasOne(d => d.TipoPago)
                    .WithMany(p => p.Transacciones)
                    .HasForeignKey(d => d.TipoPagoId)
                    .HasConstraintName("FK_dbo.Transacciones_dbo.TipoPago_TipoPagoId");

                entity.HasOne(d => d.Turno)
                    .WithMany(p => p.Transacciones)
                    .HasForeignKey(d => d.TurnoId)
                    .HasConstraintName("FK_dbo.Transacciones_dbo.Turnos_TurnoId");

                entity.HasOne(d => d.Carriles)
                    .WithMany(p => p.Transacciones)
                    .HasForeignKey(d => new { d.NumeroCapufeCarril, d.IdGare })
                    .HasConstraintName("FK_dbo.Transacciones_dbo.Carriles_NumeroCapufeCarril_IdGare");
            });

            modelBuilder.Entity<Turnos>(entity =>
            {
                entity.HasKey(e => e.TurnoId);

                entity.Property(e => e.TurnoId).ValueGeneratedNever();

                entity.Property(e => e.HoraFin).HasMaxLength(20);

                entity.Property(e => e.HoraInicio).HasMaxLength(20);

                entity.Property(e => e.NombreTurno).HasMaxLength(40);
            });
        }
    }
}
