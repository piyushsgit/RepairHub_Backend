﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.dbModels;

public partial class ServiceRequest
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    public long? ShopId { get; set; }

    [Unicode(false)]
    public string Description { get; set; } = null!;

    public long? RequestStatusId { get; set; }

    public long UserAddressId { get; set; }

    public long? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }
}