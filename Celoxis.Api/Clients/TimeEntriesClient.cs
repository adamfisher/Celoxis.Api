using Celoxis.Api.Interfaces;
using Celoxis.Api.Models;

namespace Celoxis.Api.Clients;

/// <summary>
/// Client for time entry operations
/// </summary>
public class TimeEntriesClient : EntityClient<CeloxisTimeEntry>, ITimeEntriesClient
{
    /// <inheritdoc />
    protected override string EntityPath => "/api/v2/timeEntries";

    /// <summary>
    /// Initializes a new instance of the TimeEntriesClient class
    /// </summary>
    /// <param name="client">Celoxis client instance</param>
    public TimeEntriesClient(CeloxisClient client) : base(client) { }
}