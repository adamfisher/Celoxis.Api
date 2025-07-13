using System.Threading.Tasks;
using Celoxis.Api.Interfaces;
using Celoxis.Api.Models;

namespace Celoxis.Api.Clients;

/// <summary>
/// Client for user operations
/// </summary>
public class UsersClient : EntityClient<CeloxisUser>, IUsersClient
{
    /// <inheritdoc />
    protected override string EntityPath => "/api/v2/users";

    /// <summary>
    /// Initializes a new instance of the UsersClient class
    /// </summary>
    /// <param name="client">Celoxis client instance</param>
    public UsersClient(CeloxisClient client) : base(client) { }

    /// <summary>
    /// Delete is not supported for users
    /// </summary>
    public override Task DeleteAsync(string id)
    {
        throw new System.NotSupportedException("Users cannot be deleted using the API");
    }
}