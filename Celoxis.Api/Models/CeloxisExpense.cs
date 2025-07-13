using System;

namespace Celoxis.Api.Models
{
    /// <summary>
    /// Represents an expense
    /// </summary>
    public class CeloxisExpense : CeloxisModel
    {
        /// <summary>
        /// Expense ID
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Expense URL
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Date
        /// </summary>
        public DateTimeOffset? Date { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// CeloxisProject URL or data (when expanded)
        /// </summary>
        public object Project { get; set; } = string.Empty;

        /// <summary>
        /// Task URL or data (when expanded)
        /// </summary>
        public object Task { get; set; } = string.Empty;

        /// <summary>
        /// User URL or data (when expanded)
        /// </summary>
        public object User { get; set; } = string.Empty;

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Date created
        /// </summary>
        public DateTimeOffset? Created { get; set; }

        /// <summary>
        /// Date last modified
        /// </summary>
        public DateTimeOffset? LastModified { get; set; }
    }
}
