using Celoxis.Api.Interfaces;
using Celoxis.Api.Models;

namespace Celoxis.Api.Clients
{
    /// <summary>
    /// Client for project operations
    /// </summary>
    public class ProjectsClient : EntityClient<CeloxisProject>, IProjectsClient
    {
        /// <inheritdoc />
        protected override string EntityPath => "/api/v2/projects";

        /// <summary>
        /// Initializes a new instance of the ProjectsClient class
        /// </summary>
        /// <param name="client">Celoxis client instance</param>
        public ProjectsClient(CeloxisClient client) : base(client) { }
    }
}
