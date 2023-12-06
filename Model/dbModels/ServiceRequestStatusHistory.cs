﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Model.dbModels;

public partial class ServiceRequestStatusHistory
{
    [Key]
    public long Id { get; set; }

    public long? ServiceRequest { get; set; }

    public long? RequestStatus { get; set; }

    public string? Description { get; set; }

    public long? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }
}