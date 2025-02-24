using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNI_Management.Domain.DataModels;

[Table("LeaveRequest")]
public partial class LeaveRequest
{
    [Key]
    [Column("LeaveRequestID")]
    public int LeaveRequestId { get; set; }

    public int EmployeeId { get; set; }

    public int ReportingEmployeeId { get; set; }

    [Column(TypeName = "character varying")]
    public string? ReasonForLeave { get; set; }

    public DateOnly? LeaveStartDate { get; set; }

    public int? LeaveStartDateDuration { get; set; }

    public DateOnly? LeaveEndDate { get; set; }

    public int? LeaveEndDuration { get; set; }

    public decimal? ActualLeaveDuration { get; set; }

    public decimal? TotalLeaveDuration { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public DateOnly? RequestedDate { get; set; }

    [Column(TypeName = "character varying")]
    public string PhoneNumber { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? AlternatePhoneNumber { get; set; }

    public bool? IsAdhocLeave { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [Column(TypeName = "boolean")]
    public bool? IsLeaveApproved { get; set; }

    public bool? IsAvailableOnPhone { get; set; }

    [Column(TypeName = "character varying")]
    public string? leavestatus { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("LeaveRequestEmployees")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("ReportingEmployeeId")]
    [InverseProperty("LeaveRequestReportingEmployees")]
    public virtual Employee ReportingEmployee { get; set; } = null!;
}
