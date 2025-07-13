using System;

namespace Celoxis.Api.Models
{
    /// <summary>
    /// Request to create a time entry
    /// </summary>
    public class CreateTimeEntryRequest : CeloxisModel
    {
        /// <summary>
        /// User ID or login (required)
        /// </summary>
        public string User { get; set; } = string.Empty;

        /// <summary>
        /// Work item ID (task or project) (required)
        /// </summary>
        public string WorkItem { get; set; } = string.Empty;

        /// <summary>
        /// Date (required)
        /// </summary>
        public DateTimeOffset? Date { get; set; }

        /// <summary>
        /// Hours (required)
        /// </summary>
        public decimal Hours { get; set; }

        /// <summary>
        /// Time code
        /// </summary>
        public string? TimeCode { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string? State { get; set; }
    }
}
