using System;
using System.Collections.Generic;

namespace Aurora.Backend.Users.Services.Persistence.Entities;

/// <summary>
/// Document retention tables
/// </summary>
public partial class RetentionTable
{
    public Guid Id { get; set; }

    public string DepartmentCode { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string SeriesCode { get; set; } = null!;

    public string Series { get; set; } = null!;

    public string SubseriesCode { get; set; } = null!;

    public string Subseries { get; set; } = null!;

    public int PhysicalRetentionManagement { get; set; }

    public int PhysicalRetentionCentral { get; set; }

    public bool? IsRequired { get; set; }

    public bool? HumanRights { get; set; }

    public bool? HumanitarianLaw { get; set; }

    public bool? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<RetentionTableDocumentType> RetentionTableDocumentTypes { get; set; } = new List<RetentionTableDocumentType>();
}
