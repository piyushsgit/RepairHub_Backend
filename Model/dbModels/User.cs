﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.dbModels;

public partial class User
{
    [Key]
    public long Id { get; set; }

    [StringLength(100)]
    public string First_Name { get; set; } = null!;

    [StringLength(100)]
    public string Last_Name { get; set; } = null!;

    [StringLength(20)]
    public string ContactNo { get; set; } = null!;

    [StringLength(255)]
    public string EmailId { get; set; } = null!;

    public long UserTypeId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public long? DeletedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedOn { get; set; }

    public long? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? otp { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? otpExpiryTime { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? profileImage { get; set; }

    [StringLength(100)]
    public string? password { get; set; }

    public bool? IsVarified { get; set; }
}