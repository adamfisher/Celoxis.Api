using System;

namespace Celoxis.Api
{
    /// <summary>
    /// Common date filter expressions
    /// </summary>
    public static class DateFilters
    {
        public const string Today = "Today";
        public const string Yesterday = "Yesterday";
        public const string Tomorrow = "Tomorrow";
        public const string ThisWeek = "This Week";
        public const string LastWeek = "Last Week";
        public const string NextWeek = "Next Week";
        public const string ThisMonth = "This Month";
        public const string LastMonth = "Last Month";
        public const string NextMonth = "Next Month";
        public const string ThisQuarter = "This Quarter";
        public const string LastQuarter = "Last Quarter";
        public const string NextQuarter = "Next Quarter";
        public const string ThisYear = "This Year";
        public const string LastYear = "Last Year";
        public const string NextYear = "Next Year";

        public const string Last3Days = "-3d to Today";
        public const string Last7Days = "-7d to Today";
        public const string Last30Days = "-30d to Today";
        public const string Next3Days = "Today to +3d";
        public const string Next7Days = "Today to +7d";
        public const string Next30Days = "Today to +30d";

        /// <summary>
        /// Create a date range filter
        /// </summary>
        public static string Range(DateTimeOffset? from, DateTimeOffset? to) => $"{from:yyyy-MM-dd} to {to:yyyy-MM-dd}";

        /// <summary>
        /// Create a filter for the last N days
        /// </summary>
        public static string LastNDays(int days) => $"-{days}d to Today";

        /// <summary>
        /// Create a filter for the next N days
        /// </summary>
        public static string NextNDays(int days) => $"Today to +{days}d";

        /// <summary>
        /// Create a filter for the last N weeks
        /// </summary>
        public static string LastNWeeks(int weeks) => $"-{weeks}w to Today";

        /// <summary>
        /// Create a filter for the next N months
        /// </summary>
        public static string NextNMonths(int months) => $"Today to +{months}m";
    }

    /// <summary>
    /// Entity state constants
    /// </summary>
    public static class States
    {
        /// <summary>
        /// CeloxisProject states
        /// </summary>
        public static class Project
        {
            public const string Draft = "Draft";
            public const string Active = "Active";
            public const string OnHold = "On Hold";
            public const string Completed = "Completed";
            public const string Cancelled = "Cancelled";
            public const string Opportunity = "Opportunity";
            public const string TestData = "Test Data";
        }

        /// <summary>
        /// Time entry states
        /// </summary>
        public static class TimeEntry
        {
            public const string Saved = "Saved";
            public const string PendingApproval = "Pending Approval";
            public const string Approved = "Approved";
            public const string Rejected = "Rejected";
        }
    }

    /// <summary>
    /// Priority constants
    /// </summary>
    public static class Priorities
    {
        public const string VeryHigh = "VERY_HIGH";
        public const string High = "HIGH";
        public const string Normal = "NORMAL";
        public const string Low = "LOW";
        public const string VeryLow = "VERY_LOW";
    }

    /// <summary>
    /// Billing type constants
    /// </summary>
    public static class BillingTypes
    {
        public const string None = "NONE";
        public const string FixedPrice = "FIXED_PRICE";
        public const string TimeAndMaterial = "TNM";
    }

    /// <summary>
    /// Schedule type constants
    /// </summary>
    public static class ScheduleTypes
    {
        public const string FixedUnits = "1";
        public const string FixedEffort = "2";
        public const string FixedDuration = "3";
    }

    /// <summary>
    /// Task constraint type constants
    /// </summary>
    public static class ConstraintTypes
    {
        public const string AsLateAsPossible = "ALAP";
        public const string AsSoonAsPossible = "ASAP";
        public const string FinishNoEarlierThan = "FNET";
        public const string FinishNoLaterThan = "FNLT";
        public const string MustFinishOn = "MFO";
        public const string MustStartOn = "MSO";
        public const string StartNoEarlierThan = "SNET";
        public const string StartNoLaterThan = "SNLT";
    }

    /// <summary>
    /// Health indicator constants
    /// </summary>
    public static class HealthIndicators
    {
        public const string Green = "Green";
        public const string Yellow = "Yellow";
        public const string Red = "Red";
    }

    /// <summary>
    /// Schedule health constants
    /// </summary>
    public static class ScheduleHealth
    {
        public const string OffTrack = "1";
        public const string AtRisk = "2";
        public const string Blocked = "3";
        public const string OnTrack = "4";
        public const string Future = "5";
        public const string Completed = "8";
    }

    /// <summary>
    /// Budget health constants
    /// </summary>
    public static class BudgetHealth
    {
        public const string OffTrack = "1";
        public const string AtRisk = "2";
        public const string OnTrack = "3";
    }
}
