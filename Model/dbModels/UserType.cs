﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.dbModels;

public partial class UserType
{
    [Key]
    public long Id { get; set; }

    [Column("UserType")]
    [StringLength(100)]
    public string UserType1 { get; set; } = null!;

    [StringLength(100)]
    public string Displayname { get; set; } = null!;
}