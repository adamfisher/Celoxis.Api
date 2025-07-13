using System;

namespace Celoxis.Api.Models;

/// <summary>
/// Represents a time entry
/// </summary>
public class CeloxisTimeEntry : CeloxisModel
{
    /// <summary>
    /// Time entry ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Time entry URL
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Accounting code
    /// </summary>
    public string AccountingCode { get; set; } = string.Empty;

    /// <summary>
    /// Date created
    /// </summary>
    public DateTimeOffset? Created { get; set; }

    /// <summary>
    /// Date of time entry
    /// </summary>
    public DateTimeOffset? Date { get; set; }

    /// <summary>
    /// Week of date
    /// </summary>
    public string DateWeek { get; set; } = string.Empty;

    /// <summary>
    /// Month of date
    /// </summary>
    public string DateMonth { get; set; } = string.Empty;

    /// <summary>
    /// Quarter of date
    /// </summary>
    public string DateQuarter { get; set; } = string.Empty;

    /// <summary>
    /// Year of date
    /// </summary>
    public string DateYear { get; set; } = string.Empty;

    /// <summary>
    /// Fiscal year of date
    /// </summary>
    public string DateFiscalYear { get; set; } = string.Empty;

    /// <summary>
    /// Date last modified
    /// </summary>
    public DateTimeOffset? LastModified { get; set; }

    /// <summary>
    /// Number of hours
    /// </summary>
    public decimal Hours { get; set; }

    /// <summary>
    /// Comments
    /// </summary>
    public string Comments { get; set; } = string.Empty;

    /// <summary>
    /// Time code
    /// </summary>
    public string TimeCode { get; set; } = string.Empty;

    /// <summary>
    /// State
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Work item URL or data (when expanded)
    /// </summary>
    public object WorkItem { get; set; } = string.Empty;

    /// <summary>
    /// Year-week
    /// </summary>
    public string YearWeek { get; set; } = string.Empty;

    /// <summary>
    /// Date approved
    /// </summary>
    public DateTimeOffset? ApprovedOn { get; set; }

    /// <summary>
    /// Date invoiced
    /// </summary>
    public DateTimeOffset? InvoicedOn { get; set; }

    /// <summary>
    /// Approvals
    /// </summary>
    public string Approvals { get; set; } = string.Empty;

    /// <summary>
    /// Is billable
    /// </summary>
    public string IsBillable { get; set; } = string.Empty;

    /// <summary>
    /// Bill rate
    /// </summary>
    public decimal BillRate { get; set; }

    /// <summary>
    /// Revenue
    /// </summary>
    public decimal Revenue { get; set; }

    /// <summary>
    /// Is costable
    /// </summary>
    public string IsCostable { get; set; } = string.Empty;

    /// <summary>
    /// Cost rate
    /// </summary>
    public decimal CostRate { get; set; }

    /// <summary>
    /// Cost
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// User URL or data (when expanded)
    /// </summary>
    public object User { get; set; } = string.Empty;

    /// <summary>
    /// Approver URL or data (when expanded)
    /// </summary>
    public object Approver { get; set; } = string.Empty;

    /// <summary>
    /// CeloxisProject URL or data (when expanded)
    /// </summary>
    public object Project { get; set; } = string.Empty;
}