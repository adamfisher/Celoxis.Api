using Celoxis.Api.Interfaces;
using Celoxis.Api.Models;

namespace Celoxis.Api.Clients;

/// <summary>
/// Client for app operations
/// </summary>
public class AppsClient : EntityClient<CeloxisApp>, IAppsClient
{
    /// <inheritdoc />
    protected override string EntityPath => "/api/v2/apps";

    /// <summary>
    /// Initializes a new instance of the AppsClient class
    /// </summary>
    /// <param name="client">Celoxis client instance</param>
    public AppsClient(CeloxisClient client) : base(client) { }
}