using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNI_Management.Domain.DataModels;

[Table("WorkLog")]
public partial class WorkLog
{
    [Key]
    public int WorkLogId { get; set; }

    [Column("EmployeeID")]
    public int? EmployeeId { get; set; }

    [Column(TypeName = "character varying")]
    public string? Message { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? SignOutTime { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "character varying")]
    public string? Subject { get; set; }
}
