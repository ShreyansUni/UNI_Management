using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UNI_Management.Common;
using UNI_Management.DataModels;
using domain = UNI_Management.DataModels.Domain;

namespace UNI_Management.Domain.DataContext;

public partial class ApplicationDbContext : DbContext
{
    private static IHttpContextAccessor _httpContextAccessor;
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public static void Initialize(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<domain> Domains { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeAttachment> EmployeeAttachments { get; set; }

    public virtual DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }

    public virtual DbSet<EmployeeTask> EmployeeTasks { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectAttachment> ProjectAttachments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql(ConfigItems.DBConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.BusinessId).HasName("Business_pkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("Client_pkey");
        });

        modelBuilder.Entity<domain>(entity =>
        {
            entity.HasKey(e => e.DomainId).HasName("Domain_pkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Employee_pkey");
        });

        modelBuilder.Entity<EmployeeAttachment>(entity =>
        {
            entity.HasKey(e => e.EmployeeAttachmentId).HasName("EmployeeAttachment_pkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeAttachments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeAttachment_to_Employee");
        });

        modelBuilder.Entity<EmployeeAttendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("EmployeeAttendance_pkey");
        });

        modelBuilder.Entity<EmployeeTask>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("Task_pkey");

            entity.Property(e => e.TaskId).HasDefaultValueSql("nextval('\"Task_TaskId_seq\"'::regclass)");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_to_Employee");

            entity.HasOne(d => d.Project).WithMany(p => p.EmployeeTasks).HasConstraintName("FK_Task_to_Project");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId).HasName("LeaveRequest_pkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequestEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LeaveRequest_EmployeeId_fkey");

            entity.HasOne(d => d.ReportingEmployee).WithMany(p => p.LeaveRequestReportingEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LeaveRequest_ReportingEmployeeId_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("Notification_pkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("Project_pkey");
        });

        modelBuilder.Entity<ProjectAttachment>(entity =>
        {
            entity.HasKey(e => e.ProjectAttachmentId).HasName("ProjectAttachment_pkey");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectAttachments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectAttachment_to_Project");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
