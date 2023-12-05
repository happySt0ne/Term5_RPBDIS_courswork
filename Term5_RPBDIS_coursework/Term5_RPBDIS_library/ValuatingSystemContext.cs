using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Term5_RPBDIS_library.models.tables;
using Term5_RPBDIS_library.models.views;

namespace Term5_RPBDIS_library;

public partial class ValuatingSystemContext : IdentityDbContext<IdentityUser> {
    public ValuatingSystemContext() {
        ChangeTracker.LazyLoadingEnabled = true;
        Database.EnsureCreated();
    }

    public ValuatingSystemContext(DbContextOptions<ValuatingSystemContext> options)
        : base(options) {
        ChangeTracker.LazyLoadingEnabled = true;
        Database.EnsureCreated();
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<CompareRealPlannedEfficiency> CompareRealPlannedEfficiencies { get; set; }

    public virtual DbSet<Date> Dates { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<DivisionsMark> DivisionsMarks { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<PlannedEfficiency> PlannedEfficiencies { get; set; }

    public virtual DbSet<RealEfficiency> RealEfficiencies { get; set; }

    public virtual DbSet<WholeDivisionInfo> WholeDivisionInfos { get; set; }

    public virtual DbSet<WholeEmployeeInfo> WholeEmployeeInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        ConfigurationBuilder builder = new();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection")!;
        _ = optionsBuilder
            .UseSqlServer(connectionString)
            .Options;
        optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
        optionsBuilder.UseLazyLoadingProxies();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Achievement>(entity => {
            entity.HasKey(e => e.AchievementId).HasName("PK__Achievem__276330E0772BD94E");

            entity.Property(e => e.AchievementId).HasColumnName("AchievementID");
        });

        modelBuilder.Entity<CompareRealPlannedEfficiency>(entity => {
            entity
                .HasNoKey()
                .ToView("CompareRealPlannedEfficiencies");

            entity.Property(e => e.DivisionName).HasMaxLength(30);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<Date>(entity => {
            entity.HasKey(e => e.DateId).HasName("PK__Dates__A426F253E09D6529");

            entity.Property(e => e.DateId).HasColumnName("DateID");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<Division>(entity => {
            entity.HasKey(e => e.DivisionId).HasName("PK__Division__20EFC688A932A6C0");

            entity.Property(e => e.DivisionId).HasColumnName("DivisionID");
            entity.Property(e => e.MarkId).HasColumnName("MarkID");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PlannedEfficiencyId).HasColumnName("PlannedEfficiencyID");
            entity.Property(e => e.RealEfficiencyId).HasColumnName("RealEfficiencyID");

            entity.HasOne(d => d.Mark).WithMany(p => p.Divisions)
                .HasForeignKey(d => d.MarkId)
                .HasConstraintName("FK__Divisions__MarkI__45F365D3");

            entity.HasOne(d => d.PlannedEfficiency).WithMany(p => p.Divisions)
                .HasForeignKey(d => d.PlannedEfficiencyId)
                .HasConstraintName("FK__Divisions__Plann__46E78A0C");

            entity.HasOne(d => d.RealEfficiency).WithMany(p => p.Divisions)
                .HasForeignKey(d => d.RealEfficiencyId)
                .HasConstraintName("FK__Divisions__RealE__47DBAE45");
        });

        modelBuilder.Entity<DivisionsMark>(entity => {
            entity
                .HasNoKey()
                .ToView("DivisionsMarks");

            entity.Property(e => e.DivisionName).HasMaxLength(30);
        });

        modelBuilder.Entity<Employee>(entity => {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF11880FDB5");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.AchievementId).HasColumnName("AchievementID");
            entity.Property(e => e.DivisionId).HasColumnName("DivisionID");
            entity.Property(e => e.HireDate).HasColumnType("date");
            entity.Property(e => e.MarkId).HasColumnName("MarkID");
            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasOne(d => d.Achievement).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AchievementId)
                .HasConstraintName("FK__Employees__Achie__440B1D61");

            entity.HasOne(d => d.Division).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("FK__Employees__Divis__4316F928");

            entity.HasOne(d => d.Mark).WithMany(p => p.Employees)
                .HasForeignKey(d => d.MarkId)
                .HasConstraintName("FK__Employees__MarkI__44FF419A");
        });

        modelBuilder.Entity<Mark>(entity => {
            entity.HasKey(e => e.MarkId).HasName("PK__Marks__4E30D346FA635726");

            entity.Property(e => e.MarkId).HasColumnName("MarkID");
        });

        modelBuilder.Entity<PlannedEfficiency>(entity => {
            entity.HasKey(e => e.PlannedEfficiencyId).HasName("PK__PlannedE__0642F6884D2F25DF");

            entity.Property(e => e.PlannedEfficiencyId).HasColumnName("PlannedEfficiencyID");
            entity.Property(e => e.DateId).HasColumnName("DateID");

            entity.HasOne(d => d.Date).WithMany(p => p.PlannedEfficiencies)
                .HasForeignKey(d => d.DateId)
                .HasConstraintName("FK__PlannedEf__DateI__48CFD27E");
        });

        modelBuilder.Entity<RealEfficiency>(entity => {
            entity.HasKey(e => e.RealEfficiencyId).HasName("PK__RealEffi__F21BFCE5E7E958DA");

            entity.Property(e => e.RealEfficiencyId).HasColumnName("RealEfficiencyID");
            entity.Property(e => e.DateId).HasColumnName("DateID");

            entity.HasOne(d => d.Date).WithMany(p => p.RealEfficiencies)
                .HasForeignKey(d => d.DateId)
                .HasConstraintName("FK__RealEffic__DateI__49C3F6B7");
        });

        modelBuilder.Entity<WholeDivisionInfo>(entity => {
            entity
                .HasNoKey()
                .ToView("WholeDivisionInfo");

            entity.Property(e => e.DivisionName).HasMaxLength(30);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<WholeEmployeeInfo>(entity => {
            entity
                .HasNoKey()
                .ToView("WholeEmployeeInfo");

            entity.Property(e => e.DivisionName).HasMaxLength(30);
            entity.Property(e => e.EmployeeName).HasMaxLength(30);
            entity.Property(e => e.HireDate).HasColumnType("date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
